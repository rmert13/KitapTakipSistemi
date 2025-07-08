# Kitap Takip Sistemi

 Bu proje, küçük kütüphaneler veya kişisel kitaplıklar için geliştirilmiş basit ama işlevsel bir kitap takip uygulamasıdır. Amacı; kitapların envanterini yönetmek, ödünç alma ve iade işlemlerini takip etmek ve kolay kullanılabilir bir arayüz sunmaktır.

---

## Kurulum ve Çalıştırma

1. Öncelikle projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone https://github.com/rmert13/KitapTakipSistemi.git
Visual Studio’da KitapTakipSistemi projesini açın.

NuGet paketlerini restore edin.

Web.config dosyasındaki veritabanı bağlantı ayarlarını kendi ortamınıza göre düzenleyin.

SQL Server üzerinde veritabanınızı oluşturmak için create_tables.sql dosyasındaki scripti çalıştırın.

Projeyi derleyip çalıştırın. Tarayıcıda kitapları listeleyebilir, yeni kitap ekleyebilir, ödünç alma ve iade işlemlerini yapabilirsiniz.

## Mimari Yapı

Bu proje temel olarak MVC (Model-View-Controller) yapısıyla geliştirilmiştir:

Model: Veritabanındaki tabloları temsil eden sınıflar (Kitap, Tür, Ödünç vb.)

View: Kullanıcıya gösterilen HTML sayfaları, Bootstrap ile responsive olarak tasarlandı.

Controller: Uygulama mantığını, kullanıcı isteklerini yöneten ve modellerle veritabanı arasındaki köprüyü sağlayan katman.

Veri erişimi için Entity Framework kullanıldı. Böylece veritabanı işlemleri daha kolay ve okunabilir şekilde yazıldı.

Ayrıca, kullanıcı işlemleri ve hata durumları için Serilog kütüphanesi ile loglama yapıldı. Böylece uygulamada olan biten her şey kayıt altına alınıyor.












