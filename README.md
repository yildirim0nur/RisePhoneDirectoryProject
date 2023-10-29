# RisePhoneDirectoryProject

Teknik Tasar�m

Ki�iler: Sistemde teorik anlamda s�n�rs�z say�da ki�i kayd� yap�labilecektir. Her ki�iye
ba�l� ileti�im bilgileri de yine s�n�rs�z bir bi�imde eklenebilmelidir.
Kar��lanmas� beklenen veri yap�s�ndaki gerekli alanlar a�a��daki gibidir:
	� UUID
	� Ad
	� Soyad
	� Firma
	� �leti�im Bilgisi
		o Bilgi Tipi: Telefon Numaras�, E-mail Adresi, Konum
		o Bilgi ��eri�i
Rapor: Rapor talepleri asenkron �al��acakt�r. Kullan�c� bir rapor talep etti�inde, sistem
arkaplanda bu �al��may� darbo�az yaratmadan s�ral� bir bi�imde ele alacak; rapor
tamamland���nda ise kullan�c�n�n "raporlar�n listelendi�i" endpoint �zerinden raporun
durumunu "tamamland�" olarak g�zlemleyebilmesi gerekmektedir.
Rapor basit�e a�a��daki bilgileri i�erecektir:
� Konum Bilgisi
� O konumda yer alan rehbere kay�tl� ki�i say�s�
� O konumda yer alan rehbere kay�tl� telefon numaras� say�s�
Veri yap�s� olarak da:
� UUID
� Raporun Talep Edildi�i Tarih
� Rapor Durumu (Haz�rlan�yor, Tamamland�)


Kullan�lan Teknolojiler

o .NET Core
o Git
o Postgres
o RabbitMq

�al��t�rmak i�in

1 - docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
2 - Contacts.Api ve Reports.Api projelerinde appsettings.json dosyas�ndaki pgConnectionString ve RabbitMqConnectionString d�zenlenir.
3 - Solution a sa� t�klay�p, Configure Startup Project e t�klan�r. Gelen ayar men�s�nden Multiple Project se�ilip Contacts.Api ve Reports.Api projeleri se�ilir.
4 - Ard�ndan projemiz �al��maya haz�r...