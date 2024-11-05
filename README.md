

# BoreWellManager

Bu proje,Patika Dev+ final ödevidir ve  su kuyuları açan bir firmanın arazi, kullanıcılar, belgeler ve ödeme gibi varlıklarını yönetmek amacıyla geliştirilmiş çok katmanlı bir Web API uygulamasıdır. BoreWellManager projesi, özellikle arazilerin sahiplerini, kiracıları ve şirket çalışanlarını yönetir. Aynı zamanda kimlik doğrulama ve yetkilendirme işlemleriyle kullanıcı yönetimini sağlar.

## Proje Özeti

**BoreWellManager**, su kuyuları açan bir firma için geliştirilmiş çok katmanlı bir web API uygulamasıdır. Bu API ile araziler, sahipleri ve kiracıları gibi birçok veriyi yönetebilir ve kullanıcılara farklı yetkilendirme seçenekleri sunabiliriz. Proje, kimlik doğrulama ve yetkilendirme için JWT (JSON Web Token) kullanarak güvenli bir erişim sağlar. Ayrıca, Entity Framework ile veri yönetimi ve  middleware katmanıyla kapsamlı bir backend mimarisi sağlar.

### Kullanılan Teknolojiler ve Araçlar

- **ASP.NET Core Web API** - Web API geliştirme
- **Entity Framework Core (Code First)** - Veri erişim katmanı ve veritabanı yönetimi
- **JWT (JSON Web Token)** - Kimlik doğrulama ve yetkilendirme
- **ASP.NET Core Identity veya Custom Kullanıcı Yönetimi** - Kullanıcı yönetimi
- **Repository Pattern** - Veri işlemleri için
- **Unit of Work** - Veri işlemleri yönetimi
- **Dependency Injection (DI)** - Bağımlılık yönetimi
- **Middleware** - Özel ara yazılımlar
- **Action Filters** - Özel işlemler için
- **Model Validation** - Veri doğrulama işlemleri

## Proje Mimarisi

Proje, 3 ana katmandan oluşmaktadır:

1. **Business Katmanı**: İş kurallarının ve operasyonların yönetildiği katman. Her bir `Controller` için ilgili `Service` (örneğin, `ILandService`), gerekli CRUD operasyonlarını `Task` şeklinde tanımlar.
2. **Data Katmanı**: Entity tanımları, enumlar, repository ve unit of work yapıları bu katmanda bulunur. Ayrıca migration işlemleri burada gerçekleştirilir.
3. **Web API Katmanı**: API endpoint işlemlerinin tanımlandığı katman. Bu katman üzerinden kullanıcılar ile sistemin etkileşimi sağlanır.

## API Özellikleri

Aşağıdaki API işlemleri sağlanmaktadır:

- **GET**: Kayıtları listeleme ve detay görüntüleme
- **POST**: Yeni kayıt ekleme
- **PUT**: Mevcut kayıtları güncelleme
- **PATCH**: Kısmi güncelleme işlemi
- **DELETE**: Kayıtları silme

### Kimlik Doğrulama ve Yetkilendirme

- **Kimlik Doğrulama (Authentication)**: Kullanıcılar JWT token ile doğrulanır.
- **Yetkilendirme (Authorization)**: Her kullanıcı rolüne göre yetkilendirme işlemleri yapılır.
- **Kullanıcı Yönetimi**: ASP.NET Core Custom Identitiy yönetimi kullanılarak gerçekleştirilmiştir.

## Tablolar ve İlişkileri
![tables-relation](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/diagram3.png)



### 1. Users

**Users** tablosu, sistemdeki kullanıcı bilgilerini saklar.

- **Id**: Her kullanıcıya ait benzersiz kimlik (Primary Key).
- **TC**: Kullanıcının TC kimlik numarası.
- **Name**: Kullanıcının adı.
- **Phone**: Kullanıcının telefon numarası.
- **Address**: Kullanıcının adresi.
- **UserType**: Kullanıcının tipi (örneğin, Admin, User).
- **IsResponsible**: Kullanıcının sorumluluğu olup olmadığını belirtir. (bir arazinin birden fazla sahibi olabilir ama kuyu biri adına açılabilir o yüzden bu sorumluluğu alan kişi bununla belirlenmiş oldu.)
- **CreateDate**: Kullanıcının oluşturulma tarihi.
- **ModifiedDate**: Kullanıcının son güncellenme tarihi.
- **IsDeleted**: Kullanıcının silinip silinmediğini belirtir.

### 2. Land

**Land** tablosu, arazi bilgilerini saklar.

- **Id**: Her araziye ait benzersiz kimlik (Primary Key).
- **City**: Arazi şehir bilgisi (zorunlu alan).
- **Town**: Arazi kasaba bilgisi (zorunlu alan).
- **Street**: Arazi mahalle bilgisi.
- **Block**: Arazi ada bilgisi (zorunlu alan).
- **Plot**: Arazi parsel bilgisi (zorunlu alan).
- **Location**: Arazi mevki bilgisi.
- **LandType**: Arazi tipi (zorunlu alan).
- **HasLien**: Arazi üzerinde şerh, irtifak veya beyan olup olmadığını belirtir.
- **IsCksRequired**: ÇKS gerekliliğini belirtir.
- **LienType**: Şerh tipi.
- **LandOwners**: İlişkili arazi sahipleri (LandOwnersEntity ile çoka çok ilişkilidir).
- **Wells**: İlişkili kuyu bilgileri (WellEntity ile bire çok ilişkilidir ).

### 3. LandOwners

**LandOwners** tablosu, arazilerin sahiplerini saklar.

- **LandId**: İlgili arazi ID'si (Foreign Key).
- **UserId**: Arazi sahibinin kullanıcı ID'si (Foreign Key).

### 4. Well

**Well** tablosu, kuyularla ilgili bilgileri saklar.

- **Id**: Her kuyuya ait benzersiz kimlik (Primary Key).
- **UserId**: Kuyunun sahibi olan kullanıcı ID'si (Foreign Key).
- **LandId**: Kuyunun bulunduğu arazi ID'si (Foreign Key).
- **XCordinat**: X koordinatı (sayılara ve boşluklara izin veren validasyon).
- **YCordinat**: Y koordinatı (sayılara ve boşluklara izin veren validasyon).
- **Debi**: Kuyunun debisi.
- **StaticLevel**: Kuyunun statik seviyesi.
- **DynamicLevel**: Kuyunun dinamik seviyesi.
- **Documents**: İlişkili belgeler (DocumentEntity ile bire çok ilişkilidir).

### 5. Document

**Document** tablosu, belgeleri saklar.

- **Id**: Her belgeye ait benzersiz kimlik (Primary Key).
- **WellId**: İlgili kuyu ID'si (Foreign Key).
- **PaymentId**: İlgili ödeme ID'si (Foreign Key).
- **InstitutionId**: İlgili kurum ID'si (Foreign Key).
- **Type**: Belge tipi.
- **CustomerSubmissionDate**: Müşteri gönderim tarihi.
- **InstitutionSubmissionDate**: Kuruma gönderim tarihi.
- **SignaturesReceived**: İmzaların durumu (örneğin, "Received", "Email Sent", "Not Received").
- **DeliveredToInstitution**: Belgenin kuruma teslim edilip edilmediğini belirtir.
- **IsLienCertificate**: Belgenin şerh belgesi olup olmadığını belirtir.
- **DocumentFee**: Belge ücreti.
- **FeeReceived**: Ücretin alınıp alınmadığını belirtir.
- **CreatedBy**: Belgeyi oluşturan kullanıcı.
- **ModifiedBy**: Belgeyi güncelleyen kullanıcı.
 
#### Dipnot
**Payments** ve **Documents** tabloları arasında birebir ilişki vardır. Her **Document** kaydı oluşturulduğunda, ilgili bir **Payment** kaydı da oluşturulmaktadır. Benzer şekilde, bir **Document** kaydı silindiğinde, ona ait **Payment** kaydı da otomatik olarak silinmektedir.

### 6. Payment

**Payment** tablosu, ödemeleri saklar.

- **Id**: Her ödemeye ait benzersiz kimlik (Primary Key).
- **DocumentId**: İlgili belge ID'si (Foreign Key).
- **DepositorFullName**: Ödemeyi yapan kişinin tam adı.
- **PaymentDate**: Ödeme tarihi.
- **TotalAmount**: Toplam ödeme miktarı.
- **RemaningAmount**: Kalan ödeme miktarı.
- **EmployeeWhoReceivedPayment**: Ödemeyi alan çalışan.
- **IsInstallmentPayment**: Taksitli ödeme olup olmadığını belirtir.
- **InstallmentAmount**: Taksitli ödemelerdeki taksit tutarı (nullable).
- **LastPaymentDate**: Son ödeme tarihi (nullable).

### 7. Institution

**Institution** tablosu, kurum bilgilerini saklar.

- **Id**: Her kuruma ait benzersiz kimlik (Primary Key).
- **Name**: Kurumun adı (zorunlu alan).
- **City**: Kurumun bulunduğu şehir.
- **Town**: Kurumun bulunduğu kasaba.
- **Documents**: Bu kuruma ait belgelerin listesi (DocumentEntity ile bire çok ilişkilidir).

## EndPoints
### 1. Auth
![Auth](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/auth.png)

Kullanıcılar sisteme register ile kayıt olurlar.Kullanıcılar, sisteme kayıt olduktan sonra giriş yaparak token bilgisi alabilirler. Sadece kullanıcı tipi employee olan veya  IsResponsible özelliği true olan  kullanıcılar belirli endpoint'lere erişim sağlayabilir.

### 2. Users
![Users](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Users.png)

Tc veya id bilgisine sahip her kişi kullanıcı bilgilerini görebilirken tüm kullanıcıları ve adres değişikliği ile IsResponsible değişikliğini sadece employee tipindeki kullanıcı yapabilir.


### 3. Lands
![Lands](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Lands.png)

Bu endpoint'de de get kısmı tüm kullanıcılara açıktır. Fakat post,put,delete ve patch kısımları sadece employee rolündeki kişi tarafından yapılabilir. Ayrıca Put kısmında ayrıca bir timefilter validasyonu yapılmıştır.


### 4. Wells
![Wells](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Wells.png)

Kuyulara ait verilerde herkes tarafından görüntülenebilirken , ekleme, silme ve değiştirme işlemleri sadece employee rolüne ait kullanıcıdadır.

### 5. Documents
![Documents](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Documents.png)

GetByWellId enpointine kullanıcılardan isresponsible true olan kullanıcının bakmasına izin verilmiştir. Oluşturma, silme ve değişiklik kısımlarına yine sadece employee kullanıcısı yapabilmektedir.Dökümanlar resmi kurumlara gideceği için sonradan toptan bir değişiklik yapılmaması istenmiştir o yüzden put işlemi yoktur.

### 6. Payments
![Payment](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Payments.png)

Ödemeler tablosunda da GetByName kısmına Isresponsible filtresi eklenmiştir.Oluşturma, silme ve değişiklik kısımlarına yine sadece employee kullanıcısı yapabilmektedir.

### 7. Institutions
![Institutions](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Institutions.png)

Oluşturma, silme ve değişiklik kısımlarına yine sadece employee kullanıcısı yapabilmektedir.
