using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CakeGallery
{
    public int cakeID { get; set; }
    public string nameText { get; set; }
    public string sizeText { get; set; }
    public string flavourText { get; set; }
    public string timeStamp { get; set; }
    public byte[] img1 { get; set; }
    public byte[] img2 { get; set; }
    public byte[] img3 { get; set; }
    public byte[] img4 { get; set; }

    public CakeGallery(int cakeID,string nameText, string sizeText,string flavourText, string timeStamp,byte[] img1, byte[] img2, byte[] img3, byte[] img4)
    {
        this.cakeID = cakeID;
        this.nameText = nameText;
        this.sizeText = sizeText;
        this.flavourText = flavourText;
        this.timeStamp = timeStamp;
        this.img1 = img1;
        this.img2 = img2;
        this.img3 = img3;
        this.img4 = img4;
    }
}
