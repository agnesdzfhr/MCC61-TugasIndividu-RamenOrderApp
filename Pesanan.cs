using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RamenOrderApp
{
    class Pesanan
    {
        public Menu angkaPesanan { get; set; }
        public int jumlahPesanan { get; set; }
        public int hargaPesanan { get; set; }

        public Pesanan(Menu angkaPesanan, int jumlahPesanan, int hargaPesanan)
        {
            this.angkaPesanan = angkaPesanan;
            this.jumlahPesanan = jumlahPesanan;
            this.hargaPesanan = hargaPesanan;
        }

        public void printPesanan()
        {
            Console.WriteLine(hargaPesanan);
        }

    }
}
