using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CakeGallery
{
    public int cakeID { get; set; }
    public string nameText { get; set; }
    public int numTier { get; set; }
    public int sizeText1 { get; set; }
    public int sizeText2 { get; set; }
    public int sizeText3 { get; set; }
    public string frosting { get; set; }
    public string flavourText1 { get; set; }
    public string flavourText2 { get; set; }
    public string flavourText3 { get; set; }
    public int price { get; set; }
    public string timeStamp { get; set; }
    public byte[] img1 { get; set; }
    public byte[] img2 { get; set; }
    public byte[] img3 { get; set; }
    public byte[] img4 { get; set; }

    public CakeGallery(int cakeID,string nameText, int numTier,int sizeText1, int sizeText2, int sizeText3, string frosting,string flavourText1, string flavourText2, string flavourText3, int price,string timeStamp,byte[] img1, byte[] img2, byte[] img3, byte[] img4)
    {
        this.cakeID = cakeID;
        this.nameText = nameText;
        this.numTier = numTier;
        this.sizeText1 = sizeText1;
        this.sizeText2 = sizeText2;
        this.sizeText3 = sizeText3;
        this.frosting = frosting;
        this.flavourText1 = flavourText1;
        this.flavourText2 = flavourText2;
        this.flavourText3 = flavourText3;
        this.price = price;
        this.timeStamp = timeStamp;
        this.img1 = img1;
        this.img2 = img2;
        this.img3 = img3;
        this.img4 = img4;
    }
}
