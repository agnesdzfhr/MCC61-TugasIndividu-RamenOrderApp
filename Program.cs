using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RamenOrderApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //Deklarasi Variabel
            string nama;
            int pesanan, exit;

            List<Menu> listMenu = new List<Menu>();
            listMenu.Add(new Menu("Paket Ramen 1", 30000));
            listMenu.Add(new Menu("Paket Ramen 2", 32000));
            listMenu.Add(new Menu("Paket Ramen 3", 35000));
            listMenu.Add(new Menu("Gyoza        ", 20000));
            listMenu.Add(new Menu("Ocha         ", 10000));
            listMenu.Add(new Menu("Milk Tea     ", 15000));

            List<Pesanan> listPesanan = new List<Pesanan>();


            //Main Program
            Console.WriteLine("Masukkan Nama Kamu: ");
            nama = Convert.ToString(Console.ReadLine());
            exit = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Hai {nama}");
                MenuUtama(listMenu);
                pesanan = int.Parse(Console.ReadLine());

                switch (pesanan)
                {
                    case 1:
                        InputMenu(listMenu, 0, listPesanan);
                        break;
                    case 2:
                        InputMenu(listMenu, 1, listPesanan);
                        break;
                    case 3:
                        InputMenu(listMenu, 2, listPesanan);
                        break;
                    case 4:
                        InputMenu(listMenu, 3, listPesanan);
                        break;
                    case 5:
                        InputMenu(listMenu, 4, listPesanan);
                        break;
                    case 6:
                        InputMenu(listMenu, 5, listPesanan);
                        break;
                    case 0:
                        exit = CekPesanan(exit, listMenu, listPesanan);
                        break;
                }
            } while (exit == 0);
        }

        private static int CekPesanan(int exit, List<Menu> listMenu, List<Pesanan> listPesanan)
        {
            int exitCP = 0;
            do
            {
                Console.Clear();
                checkpoint:
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("                 Total Pesanan Anda                                    ");
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("        Pesanan		|    Jumlah	|	Harga");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                if (listPesanan != null)
                {
                    for (int iPesan = 0; iPesan < listPesanan.Count & iPesan < listMenu.Count; iPesan++)
                    {
                        Console.WriteLine($"{iPesan + 1}. {listPesanan[iPesan].angkaPesanan.namaMenu}        |       {listPesanan[iPesan].jumlahPesanan}      |    {listPesanan[iPesan].hargaPesanan}");
                    }
                }


                int bayar = 0;
                for (int iBayar = 0; iBayar < listPesanan.Count; iBayar++)
                {
                    bayar += listPesanan[iBayar].hargaPesanan;
                }
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"Total Pembayaran: {bayar}");
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("");
                Console.WriteLine("Tekan 8 untuk memesan lagi, tekan 9 untuk menghapus pesanan, tekan 0 untuk selesai memesan");
                
                int pilih = int.Parse(Console.ReadLine());
                int exitHapus = 0;
                while(exitHapus==0)
                {
                    if (pilih == 8)
                    {
                        exitHapus = 1;
                        exitCP = 1;
                    }
                    else if (pilih == 9)
                    {
                        Console.WriteLine("");
                        Console.Write("Masukkan nomor pesanan yang ingin dihapus: ");
                        try
                        {
                            int hapus = int.Parse(Console.ReadLine()) - 1;
                            listPesanan.RemoveAt(hapus);
                            Console.Clear();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("");
                            MessageNotification(false, "Periksa kembali nomor pesanan yang ingin anda hapus!!");
                            Console.WriteLine("\nTekan Enter dan pilih ulang pilihan diatas");
                            ConsoleKeyInfo keyInfo;
                            while (true)
                            {
                                keyInfo = Console.ReadKey(true);
                                if (keyInfo.Key == ConsoleKey.Escape)
                                    continue;
                                break;
                            }
                            Console.Clear();
                        }


                        goto checkpoint;

                    }
                    else if (pilih == 0)
                    {
                        MessageNotification(true, "Silahkan melakukan pembayaran ke kasir!");
                        MessageNotification(true, "Terima kasih telah memesan:)");
                        exitHapus = 1;
                        exitCP = 1;
                        exit = 1;
                    }

                } 
            } while (exitCP == 0);
            return exit;
        }

        private static void InputMenu(List<Menu> listMenu, int nomorMenu, List<Pesanan> listPesanan)
        {
            int jumlahPesanan, hargaPesanan, id;

            Console.WriteLine("Masukkan Jumlah Pesanan: ");
            jumlahPesanan = int.Parse(Console.ReadLine());
            hargaPesanan = totalHargaPesanan(listMenu, nomorMenu, jumlahPesanan);
            List<Menu> namaPesanan = listPesanan.Select(lp => lp.angkaPesanan).ToList();
            id = namaPesanan.IndexOf(listMenu[nomorMenu]);
            if (id != -1)
            {
                listPesanan[id].jumlahPesanan += jumlahPesanan;
                listPesanan[id].hargaPesanan += hargaPesanan;
            }
            else
            {
                listPesanan.Add(new Pesanan(listMenu[nomorMenu], jumlahPesanan, hargaPesanan));
            }

            Console.WriteLine("Terpesan:)");
            Console.WriteLine("Enter untuk memesan lagi");
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                break;
            }

            static int totalHargaPesanan(List<Menu> listMenu, int nomorMenu, int jumlahPesanan)
            {
                return jumlahPesanan * listMenu[nomorMenu].hargaMenu;
            }
        }

        private static void MenuUtama(List<Menu> listMenu)
        {
            Console.WriteLine("Selamat Datang di Warung Ramen!"); ;
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("Silahkan Pilih Pesananmu");
            for (int i = 0; i < listMenu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. { listMenu[i].namaMenu}:    { ToRupiah(listMenu[i].hargaMenu)}");
            }
            Console.WriteLine("");
            Console.WriteLine("0. Cek Pesanan");
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("Masukkan Angka Pilihan Anda:");

            static string ToRupiah(int harga)
            {
                return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp{0:N}", harga);
            }


        }
        private static void MessageNotification(bool status, string message)
        {


            if (status)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t         {message}          \t");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t         {message}          \t");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
