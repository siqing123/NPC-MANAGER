using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextureAtlasLib
{
    public class Spritesheet
    {
        public int Columns { get; set; }
        public String OutputPath { get; set; }
        public String InputPath { get; set; }

        /// <summary>
        /// Checks if the properties are set correctly. If it is not correct, then it will throw an exception.
        /// </summary>
        private void Validate()
        {
            if (string.IsNullOrEmpty(InputPath))
            {
                throw new Exception("The input path cannot be empty");
            }

            if (string.IsNullOrEmpty(OutputPath))
            {
                throw new Exception("The output path cannot be empty");
            }

            if (!Directory.Exists(InputPath))
            {
                throw new DirectoryNotFoundException("The input path is either not a directory or does not exist.");
            }
            else
            {
                if (Directory.GetFiles(InputPath).Length == 0)
                {
                    throw new Exception("The input directory does not contain any files.");
                }
            }

            if (Columns < 1)
            {
                throw new Exception("Column size must be at least 1");
            }

        }

        /// <summary>
        /// Generates the a texture atlas and meta data.
        /// </summary>
        /// <param name="overwrite">enable to allow the output file to be overwritten</param>
        /// 
        public void Generate(bool overwrite)
        {
            Validate();

            if (overwrite)
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
            }
            else
            {
                if (File.Exists(OutputPath))      
                {
                    throw new Exception("The output file already exists.");
                }
            }

            // Logic was from: https://forum.unity.com/threads/quick-and-dirty-c-sprite-sheet-generator.436802/
            // Retrieve the png input files in the Input Path specified.
            string[] files = Directory.GetFiles(InputPath, "*.png");
            int fileCount = files.Length;
            if (fileCount > 0)
            {
                // Determines the largest sprite and use that as the cell size.
                int maxWidth = 0;
                int maxHeight = 0;
                foreach (string f in files)
                {
                    Image img = Image.FromFile(f);
                    maxWidth = Math.Max(maxWidth, img.Width);
                    maxHeight = Math.Max(maxHeight, img.Height);
                    img.Dispose();
                }

                if (fileCount < Columns)
                {
                    // Single row, reducing column count to match file count.
                    Columns = fileCount;
                }

                int width = Columns * maxWidth;
                int rows = (fileCount / Columns) + ((fileCount % Columns > 0) ? 1 : 0);
                int height = rows * maxHeight;

                // Console.WriteLine("Output: {0} rows, {1} cols, {2} x {3} resolution.", rows, Columns, width, height);

                List<SpriteInfo> spriteInfos = new List<SpriteInfo>();
                Bitmap sheet = new Bitmap(width, height);
                using (Graphics gfx = Graphics.FromImage(sheet))
                {
                    int col = 0;
                    int row = 0;
                    foreach (string f in files)
                    {
                        // Sprite Info intialization
                        SpriteInfo sprite = new SpriteInfo();
                        sprite.Name = System.IO.Path.GetFileNameWithoutExtension(f);

                        Image img = Image.FromFile(f);

                        int x = (col * maxWidth) + (maxWidth / 2 - img.Width / 2);
                        int y = (row * maxHeight) + (maxHeight / 2 - img.Height / 2);

                        Rectangle srcRect = new Rectangle(0, 0, img.Width, img.Height);
                        Rectangle destRect = new Rectangle(x, y, img.Width, img.Height);

                        // Populate the coordinates for the sprite.
                        sprite.Left = x;
                        sprite.Top = y;
                        sprite.Right = x + img.Width;
                        sprite.Bottom = y + img.Height;

                        gfx.DrawImage(img, destRect, srcRect, GraphicsUnit.Pixel);

                        img.Dispose();

                        col++;
                        if (col == Columns)
                        {
                            col = 0;
                            row++;
                        }
                    }
                }

                //string exportedMetaData = System.IO.Path.GetFileNameWithoutExtension(OutputPath) + ".json";
                //string json = JsonConvert.SerializeObject(spriteInfos);
                //System.IO.File.WriteAllText(exportedMetaData, json);

                sheet.Save(OutputPath);
            }
        }

    }
}
