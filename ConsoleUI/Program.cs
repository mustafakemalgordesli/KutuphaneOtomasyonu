using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Json;
using Entities.Entities;
using Service.Abstract;
using Service.Concrete;
using System;
using System.Threading;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IMemberService _memberService = new MemberManager(MemberDal.Instance);
            IBookService _bookService = new BookManager(BookDal.Instance);
            IBorrowService _borrowService = new BorrowManager(BorrowDal.Instance);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("0 : Çıkış");
                Console.WriteLine("1 : Üye Ekle");
                Console.WriteLine("2 : Üye Sil");
                Console.WriteLine("3 : Kitap Ekle");
                Console.WriteLine("4 : Kitap Sil");
                Console.WriteLine("5 : Kitapları Listele");
                Console.WriteLine("6 : Kitap Teslim Al");
                Console.WriteLine("7 : Kitap İade Et");
                Console.Write("Seçiminiz : ");
                string option = Console.ReadLine();
                Thread.Sleep(1000);
                Console.Clear();
                if (OptionControl(option))
                {
                    Console.WriteLine("Hatalı seçim yaptınız.");
                    Thread.Sleep(1000);
                    continue;
                }
                switch (option)
                {
                    case "0":
                        Console.WriteLine("Çıkış Yapıldı!");
                        Environment.Exit(1);
                        break;
                    case "1":
                        Member addedMember = new Member();
                        Console.Write("Adı : ");
                        addedMember.FirstName = Console.ReadLine();
                        Console.Write("Soyadı : ");
                        addedMember.LastName = Console.ReadLine();
                        Console.Write("E-posta : ");
                        addedMember.Email = Console.ReadLine();
                        Console.Write("Telefon Numarası : ");
                        addedMember.PhoneNumber = Console.ReadLine();
                        Console.Write("Adresi : ");
                        addedMember.AdressDetails = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            var result = _memberService.Add(addedMember);
                            if(result.Success)
                            {
                                Console.WriteLine(result.Message);
                            }
                            else
                            {
                                Console.WriteLine(result.Message);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Üye ekleme işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;
                    case "2":
                        Console.Write("Silinecek üyenin e-posta adresini giriniz : ");
                        string _email = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            var result = _memberService.GetByEmail(_email);
                            if (result.Success)
                            {
                                var deletedResult = _memberService.Delete(result.Data.Id);
                                Console.WriteLine(deletedResult.Message);

                            }
                            else
                            {
                                Console.WriteLine(result.Message);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Üye silme işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;
                    case "3":
                        Book addedBook = new Book();
                        Console.Write("Kitap adı : ");
                        addedBook.BookName = Console.ReadLine();
                        Console.Write("Kitabın kategorisi : ");
                        addedBook.CategoryName = Console.ReadLine();
                        Console.Write("Kitap yazarının ismi : ");
                        addedBook.AuthorName = Console.ReadLine();
                        Console.Write("Kitabın Barkod Numarası : ");
                        addedBook.BarcodeNo = Console.ReadLine();
                        Console.Write("Kitabın ISBN numarası : ");
                        addedBook.ISBN = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            var result = _bookService.Add(addedBook);
                            Console.WriteLine(result.Message);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Kitap ekleme işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;
                    case "4":
                        Console.Write("Silinecek kitabın barkod numarasını giriniz : ");
                        string deletedBookBarcodeNo = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            var result = _bookService.GetByBarcodeNo(deletedBookBarcodeNo);
                            if (result.Success)
                            {
                                var deletedResult = _bookService.Delete(result.Data.Id);
                                Console.WriteLine(deletedResult.Message);
                            }
                            else
                            {
                                Console.WriteLine(result.Message);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Kitap silme işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Tüm kitaplar : ");
                        int b = 1;
                        foreach (var book in _bookService.GetAll().Data)
                        {
                            Console.WriteLine(b + ". Kitap Adı : {0} - Barkod Numarası : {1}", book.BookName, book.BarcodeNo);
                            b++;
                        }
                        Console.Write("Geri dönmek için herhangi bir tuşa basınız...");
                        Console.ReadKey();
                        break;
                    case "6":
                        try
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.Write("Teslim alınacak kitabın barkod numarasını giriniz : ");
                                string _bookBarcodeNo = Console.ReadLine();
                                var borrowBookResult = _bookService.GetByBarcodeNo(_bookBarcodeNo);
                                if (!borrowBookResult.Success)
                                {
                                    Console.Clear();
                                    Console.WriteLine(borrowBookResult.Message);
                                    Thread.Sleep(1500);
                                    continue;
                                }
                                Console.Write("Teslim alacak üyenin e-posta adresi : ");
                                var _memberMail = Console.ReadLine();
                                var borrowMemberResult = _memberService.GetByEmail(_memberMail);
                                if (!borrowBookResult.Success)
                                {
                                    Console.Clear();
                                    Console.WriteLine(borrowMemberResult.Message);
                                    Thread.Sleep(1500);
                                    continue;
                                }
                                Console.Clear();
                                Borrow _borrow = new Borrow()
                                {
                                    BookId = borrowBookResult.Data.Id,
                                    BorrowDate = DateTime.Now,
                                    Status = true,
                                    UserId = borrowMemberResult.Data.Id,
                                };
                                var result = _borrowService.Add(_borrow);
                                Console.WriteLine(result.Message);
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Kitap teslim alma işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;
                    case "7":
                        try
                        {
                            Console.WriteLine("İade edilecek kitabın barkod numarasını giriniz : ");
                            string _bookBarcodeNo = Console.ReadLine();
                            var bookResult = _bookService.GetByBarcodeNo(_bookBarcodeNo);
                            Console.Clear();
                            if (!bookResult.Success)
                            {
                                Console.WriteLine(bookResult.Message);
                                break;
                            }
                            var borrowResult = _borrowService.GetByBookId(bookResult.Data.Id);
                            if (!borrowResult.Success)
                            {
                                Console.WriteLine(borrowResult.Message);
                                break;
                            }
                            borrowResult.Data.ReturnDate = DateTime.Now;
                            borrowResult.Data.Status = false;
                            var returnedBorrowResult = _borrowService.Update(borrowResult.Data);
                            Console.WriteLine(returnedBorrowResult.Message);
                            Thread.Sleep(1000);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Kitap iade etme işlemi başarısız oldu. Lütfen tekrar deneyiniz.");
                        }
                        break;

                }

                Thread.Sleep(2000);
            }
        }

        static bool OptionControl(string option)
        {
            if (option != "0" && option != "1" && option != "2" && option != "3"
                && option != "4" && option != "5" && option != "6" &&
                option != "7" && option != "8" && option != "9")
                return true;
            return false;
        }
    }
}
