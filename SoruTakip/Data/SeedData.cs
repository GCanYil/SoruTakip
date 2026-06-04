using Microsoft.EntityFrameworkCore;
using SoruTakip.Models;

namespace SoruTakip.Data;

public class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Subjects.Any()) return;

        var subjects = new List<Subject>
        {
            new Subject()
            {
                Name = "Matematik",
                Topics = new List<Topic>
                {
                    new Topic { Name = "Temel Kavramlar" },
                    new Topic { Name = "Sayı Basamakları" },
                    new Topic { Name = "Bölme - Bölünebilme Kuralları" },
                    new Topic { Name = "Asal Çarpanlara Ayırma" },
                    new Topic { Name = "EBOB - EKOK" },
                    new Topic { Name = "Rasyonel Sayılar" },
                    new Topic { Name = "Ondalıklı Sayılar" },
                    new Topic { Name = "Basit Eşitsizlikler" },
                    new Topic { Name = "Mutlak Değer" },
                    new Topic { Name = "Üslü Sayılar" },
                    new Topic { Name = "Köklü Sayılar" },
                    new Topic { Name = "Çarpanlara Ayırma" },
                    new Topic { Name = "Birinci Dereceden Denklemler" },
                    new Topic { Name = "Oran - Orantı" },
                    new Topic { Name = "Problemler" },
                    new Topic { Name = "Mantık" },
                    new Topic { Name = "Kümeler" },
                    new Topic { Name = "Fonksiyonlar" },
                    new Topic { Name = "İkinci Dereceden Denklemler" },
                    new Topic { Name = "Permütasyon - Kombinasyon" },
                    new Topic { Name = "Binom Açılımı" },
                    new Topic { Name = "Olasılık" },
                    new Topic { Name = "İstatistik" },
                    new Topic { Name = "Polinomlar" }
                }
            },
            new Subject
            {
                Name = "Fizik",
                Topics = new List<Topic>
                {
                    new Topic { Name = "Fizik Bilimine Giriş" },
                    new Topic { Name = "Madde ve Özellikleri" },
                    new Topic { Name = "Sıvıların Kaldırma Kuvveti" },
                    new Topic { Name = "Basınç" },
                    new Topic { Name = "Isı, Sıcaklık ve Genleşme" },
                    new Topic { Name = "Kuvvet ve Hareket" },
                    new Topic { Name = "Dinamik" },
                    new Topic { Name = "İş, Güç ve Enerji" },
                    new Topic { Name = "Elektrik" },
                    new Topic { Name = "Manyetizma" },
                    new Topic { Name = "Dalgalar" },
                    new Topic { Name = "Optik" },
                    new Topic { Name = "Vektörler" },
                    new Topic { Name = "Kuvvet, Tork ve Denge" },
                    new Topic { Name = "Kütle Merkezi" },
                    new Topic { Name = "Basit Makineler" },
                    new Topic { Name = "Hareket" },
                    new Topic { Name = "Newton'un Hareket Yasaları" },
                    new Topic { Name = "İş, Güç ve Enerji II" },
                    new Topic { Name = "Atışlar" },
                    new Topic { Name = "İtme ve Momentum" },
                    new Topic { Name = "Elektrik Alan ve Potansiyel" },
                    new Topic { Name = "Paralel Levhalar ve Sığa" },
                    new Topic { Name = "Manyetik Alan ve Manyetik Kuvvet" },
                    new Topic { Name = "İndüksiyon, Alternatif Akım ve Transformatörler" },
                    new Topic { Name = "Çembersel Hareket" },
                    new Topic { Name = "Dönme, Yuvarlanma ve Açısal Momentum" },
                    new Topic { Name = "Kütle Çekim ve Kepler Yasaları" },
                    new Topic { Name = "Basit Harmonik Hareket" },
                    new Topic { Name = "Dalga Mekaniği ve Elektromanyetik Dalgalar" },
                    new Topic { Name = "Atom Modelleri" },
                    new Topic { Name = "Büyük Patlama ve Parçacık Fiziği" },
                    new Topic { Name = "Radyoaktivite" },
                    new Topic { Name = "Özel Görelilik" },
                    new Topic { Name = "Kara Cisim Işıması" },
                    new Topic { Name = "Fotoelektrik Olay ve Compton Olayı" },
                    new Topic { Name = "Modern Fiziğin Teorideki Uygulamaları" }
                }
            },
            new Subject
            {
                Name = "Kimya",
                Topics = new List<Topic>
                {
                    new Topic { Name = "Kimyasal Türler Arası Etkileşimler" },
                    new Topic { Name = "Atom ve Periyodik Sistem" },
                    new Topic { Name = "Kimya Bilimi" },
                    new Topic { Name = "Maddenin Halleri" },
                    new Topic { Name = "Doğa ve Kimya" },
                    new Topic { Name = "Kimyanın Temel Kanunları" },
                    new Topic { Name = "Kimyasal Hesaplamalar" },
                    new Topic { Name = "Karışımlar" },
                    new Topic { Name = "Asitler, Bazlar ve Tuzlar" },
                    new Topic { Name = "Kimya Her Yerde" },
                    new Topic { Name = "Kimyasal Tepkimelerde Enerji" },
                    new Topic { Name = "Kimyasal Tepkimelerde Hız" },
                    new Topic { Name = "Kimyasal Tepkimelerde Denge" },
                    new Topic { Name = "Modern Atom Teorisi" },
                    new Topic { Name = "Gazlar" },
                    new Topic { Name = "Sıvı Çözeltiler ve Çözünürlük" },
                    new Topic { Name = "Karbon Kimyasına Giriş" },
                    new Topic { Name = "Organik Bileşikler" },
                    new Topic { Name = "Kimya ve Elektrik" },
                    new Topic { Name = "Enerji Kaynakları ve Bilimsel Gelişmeler" }
                }
            },
            new Subject
            {
                Name = "Biyoloji",
                Topics = new List<Topic>
                {
                    new Topic { Name = "Canlıların Ortak Özellikleri" },
                    new Topic { Name = "Canlıların Temel Bileşenleri" },
                    new Topic { Name = "Hücre ve Organeller - Madde Geçişleri" },
                    new Topic { Name = "Canlıların Sınıflandırılması" },
                    new Topic { Name = "Hücrede Bölünme - Üreme" },
                    new Topic { Name = "Kalıtım" },
                    new Topic { Name = "Bitki Biyolojisi" },
                    new Topic { Name = "Ekosistem" },
                    new Topic { Name = "Sinir Sistemi" },
                    new Topic { Name = "Endokrin Sistem ve Hormonlar" },
                    new Topic { Name = "Duyu Organları" },
                    new Topic { Name = "Destek ve Hareket Sistemi" },
                    new Topic { Name = "Sindirim Sistemi" },
                    new Topic { Name = "Dolaşım ve Bağışıklık Sistemi" },
                    new Topic { Name = "Solunum Sistemi" },
                    new Topic { Name = "Üriner Sistem" },
                    new Topic { Name = "Üreme Sistemi" },
                    new Topic { Name = "Komünite Ekolojisi" },
                    new Topic { Name = "Popülasyon Ekolojisi" },
                    new Topic { Name = "Nükleik Asitler" },
                    new Topic { Name = "Genetik Şifre ve Protein Sentezi" },
                    new Topic { Name = "Canlılık ve Enerji" },
                    new Topic { Name = "Fotosentez" },
                    new Topic { Name = "Kemosentez" },
                    new Topic { Name = "Hücresel Solunum" },
                    new Topic { Name = "Bitki Biyolojisi" },
                    new Topic { Name = "Canlılar ve Çevre" },
                }
            }
        };
        context.Subjects.AddRange(subjects);
        context.SaveChanges();
    }
}