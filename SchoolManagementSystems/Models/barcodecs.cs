using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SchoolManagementSystems.Models
{
    public class barcodecs
    {
        PrivateFontCollection myFonts; //CREATE A FONT COLLECTION
        public string generateBarcode(int bookid)
        {
            try
            {
                string[] charPool = "1-2-3-4-5-6-7-8-9-0".Split('-');
                StringBuilder rs = new StringBuilder();
                
                int length = 0;
                int lengthno = bookid.ToString().Length;
                switch (lengthno)
                {
                    case 1:
                        length = 5;
                        Random rnd = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                    case 2:
                        length = 4;
                        Random rnd1 = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd1.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                    case 3:
                        length = 3;
                        Random rnd2 = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd2.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                    case 4:
                        length = 2;
                        Random rnd3 = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd3.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                    case 5:
                        length = 1;
                        Random rnd4 = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd4.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                    case 6:
                        length = 0;
                        rs.Append(bookid.ToString());
                        break;
                    default:
                        length = 6;
                        Random rnd5 = new Random();
                        rs.Append(bookid.ToString());
                        while (length-- > 0)
                        {
                            int index = (int)(rnd5.NextDouble() * charPool.Length);
                            if (charPool[index] != "-")
                            {
                                rs.Append(charPool[index]);
                                charPool[index] = "-";
                            }
                            else
                                length++;
                        }
                        break;
                }
                
             
                return rs.ToString();

            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
            }
            return "";
        }

        public Byte[] getBarcodeImage(string barcode, string file)
        {

            try
            {
                byte[] byteImage = null;
                string fontFile = HttpContext.Current.Server.MapPath("~/fonts/IDAutomationHC39M.ttf");
                FontFamily family = LoadFontFamily(fontFile, out myFonts);

                using (Bitmap bitMap = new Bitmap(barcode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                      
                        Font oFont = new Font(family, 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byteImage = ms.ToArray();

                        Convert.ToBase64String(byteImage);

                    }
                    return byteImage;
                }
            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
            }
            return null;
        }

        private FontFamily LoadFontFamily(string fileName, out PrivateFontCollection _myFonts)
        {
            //IN MEMORY _myFonts point to the myFonts created in the load event 11 lines up.
            _myFonts = new PrivateFontCollection();//here is where we assing memory space to myFonts
            _myFonts.AddFontFile(fileName);//we add the full path of the ttf file
            return _myFonts.Families[0];//returns the family object as usual.
        }
    }
}