using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Error_Diffusion
{
    //1. incorrect position of value -> would be this fuck up
    //2. incorrect read pixel -> proof with https://www.dcode.fr/image-pixel-reader it's correctly read image
    internal class Program
    {
        const string Path = @"D:\Csharp\Error_Diffusion\lena_gray.bmp";
        
        public static void Main(string[] args)
        {
            Bitmap inputImage = new Bitmap(Path,true);
            int imgWidth = inputImage.Width;
            int imgHeight = inputImage.Height;
            Bitmap outputImage = new Bitmap(imgWidth,imgHeight);
            
            // Bitmap own1_image = new Bitmap(@"D:\Csharp\Error_Diffusion\Error_Diffusion\bin\Debug\lena_error.bmp", false);
            // Bitmap own2_image = new Bitmap(@"D:\Csharp\Error_Diffusion\Error_Diffusion\bin\Debug\lena_error2.bmp", false);
            
            int[,] data = new int[inputImage.Height+2,inputImage.Width+2];
            int[,] ans = SingleThread(data,imgHeight+1,imgWidth+1);
            MappingImage(ans,outputImage);
        }
        
        public static int[,] SingleThread(int[,] img,int rowBound,int colBound)
        {
            int[,] OutputImage = (int[,])img.Clone();
            Console.WriteLine(OutputImage[0,0]+"++++++++++++++++");
            int[,] InputImage = (int[,])img.Clone();
            int err;
            
            for (int row = 1; row < rowBound; row++) {
                for (int col = 1; col < colBound; col++) {
                    OutputImage[row,col] = (InputImage[row,col] < 128 ? 0 : 1);
                    err = (InputImage[row,col] - (255 * OutputImage[row,col]));
                    InputImage[row,col+1] += ((err * 7) / 16);
                    InputImage[row+1,col-1] += ((err * 3) / 16);
                    InputImage[row+1,col] += ((err * 5) / 16);
                    InputImage[row+1,col+1] += ((err * 1) / 16);
                }
            }

            return OutputImage;

        }

        private static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        
        public static bool IsSameContent(Bitmap firstBMP,Bitmap secondBMP)
        {
            byte[] firstImage = ImageToByte2(firstBMP);
            byte[] secondImage = ImageToByte2(secondBMP);
            return firstImage.SequenceEqual(secondImage);
        }

        public static int[,] GetPixelData(Bitmap img)
        {
            int[,] data = new int[img.Height+2,img.Width+2];
            for (int i = 1; i < img.Height + 1; i++)
            {
                if (i >= img.Height) continue;
                for (int j = 1; j < img.Width + 1; j++)
                {
                    if (j >= img.Width) continue;
                    // Console.WriteLine($"row: {i} col: {j} value: {bmp.GetPixel(j - 1, i - 1).G}");
                    // below line fuck up
                    data[i, j] = img.GetPixel(j - 1, i - 1).G;
                }
            }

            return data;
        }

        public static void MappingImage(int[,] ans,Bitmap final)
        {
            for (int i = 1; i < ans.GetLength(0) - 1; i++)
            {
                if (i >= ans.GetLength(0) - 1) continue;
                for (int k = 1; k < ans.GetLength(1) - 1; k++)
                {
                    if (k >= ans.GetLength(1) - 1) continue;
                    // Console.WriteLine($"row: {i} col: {k} value: {ans[i,k]}");
                    final.SetPixel(k - 1, i - 1, ans[i, k] == 1 ? Color.White : Color.Black);
                }
            }
            final.Save("lena_error2.bmp",ImageFormat.Bmp);
        }
    }
}

class Multi
{
    private int thread1_col;
    

    public void DoJob()
    {
        Thread t1; # # # # # #
        Thread t2; # # # #
        Thread t3; # #
        // in t2
        while (row < thread1_row - 2^(tn - t1))
        {
            
        }
    }
}