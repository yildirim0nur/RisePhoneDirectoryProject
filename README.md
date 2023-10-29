# RisePhoneDirectoryProject

Teknik Tasarým

Kiþiler: Sistemde teorik anlamda sýnýrsýz sayýda kiþi kaydý yapýlabilecektir. Her kiþiye
baðlý iletiþim bilgileri de yine sýnýrsýz bir biçimde eklenebilmelidir.
Karþýlanmasý beklenen veri yapýsýndaki gerekli alanlar aþaðýdaki gibidir:
	• UUID
	• Ad
	• Soyad
	• Firma
	• Ýletiþim Bilgisi
		o Bilgi Tipi: Telefon Numarasý, E-mail Adresi, Konum
		o Bilgi Ýçeriði
Rapor: Rapor talepleri asenkron çalýþacaktýr. Kullanýcý bir rapor talep ettiðinde, sistem
arkaplanda bu çalýþmayý darboðaz yaratmadan sýralý bir biçimde ele alacak; rapor
tamamlandýðýnda ise kullanýcýnýn "raporlarýn listelendiði" endpoint üzerinden raporun
durumunu "tamamlandý" olarak gözlemleyebilmesi gerekmektedir.
Rapor basitçe aþaðýdaki bilgileri içerecektir:
• Konum Bilgisi
• O konumda yer alan rehbere kayýtlý kiþi sayýsý
• O konumda yer alan rehbere kayýtlý telefon numarasý sayýsý
Veri yapýsý olarak da:
• UUID
• Raporun Talep Edildiði Tarih
• Rapor Durumu (Hazýrlanýyor, Tamamlandý)


Kullanýlan Teknolojiler

o .NET Core
o Git
o Postgres
o RabbitMq

Çalýþtýrmak için

1 - docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
2 - Contacts.Api ve Reports.Api projelerinde appsettings.json dosyasýndaki pgConnectionString ve RabbitMqConnectionString düzenlenir.
3 - Solution a sað týklayýp, Configure Startup Project e týklanýr. Gelen ayar menüsünden Multiple Project seçilip Contacts.Api ve Reports.Api projeleri seçilir.
4 - Ardýndan projemiz çalýþmaya hazýr...