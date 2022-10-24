# RiseContact


#start with docker-compose

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

Refit
Cap
RabbitMq


Burada rapor oluşturmak için 3 farklı endpoin oluşturdum. Temel ve önemli farklılıkları var!

![contact](https://user-images.githubusercontent.com/113763553/197459375-2aced5a9-1e53-40ba-96ee-2964779297b9.jpg)


Contact Microservice:
1- Yol: Buradaki CreateReportRequest (Contact Microservisinde) endpointi rapor isteğini bir rapor guid oluştururak rabbitmq üzerinden report microservice iletir.
Burada microservice direkt olarak veritabanından veriyi okur ve reporu oluştur ve kaydeder.
(Event Driven Design)

2- Yol:
Buradaki CreateReportRequest (Contact Microservisinde) endpoint Veriyi alır RabbitMq üzerinden Report microservice iletir.Report microservisi veriyi kuyruktan alır ve raporu oluşturur.
(Event Driven Design)

3- Yol:

Buradaki CreateReportWithRest (Report Microservisinde) endpoint Veriyi almak için Contact microservice ile (Rest) ile haberleşir. Veriyi alır RabbitMq kuyruğa iletir. Report microservice kuyruğu dinler gelen datayla raporu oluşturur ve kaydeder.

(Rest) 

![report](https://user-images.githubusercontent.com/113763553/197459388-e26d5413-0571-4885-851d-bcd85c1543ca.jpg)


Burada amaç microservice haberleşmesi olduğu için 3 yol bunu sağlıyor, case bunu istiyor. Ancak önemli bir konu var, microservis ler haberleşsin tamam. 

 Amacımız veriyi alıp bir rapor oluşturmak sa veriyi contact tan gelen bir isteği report a bir message broker  ile bildirip  gerekli veriyi db den okuması ve raporu oluşturması daha sağlıklı (Tabiki db erişim yapabiliyor olaması şartıyla)
 
 Bazı microservis yaklaşımlarında aynı db kullanmama gibi durumlar bile birbirine bağımlı verileri gerektiği durumlarda iki serviste te bu veriler tutulur.
 Çünkü servisin birisi durması durumunda diğeri çalışamayacak duruma gelir. aslında bir değil 2 servis te işlevsiz hale gelecektir.
 
 Verinin büyük olması durumunda da message broker üzerinden bu veriyi taşımak ta maliyet.

Cap Kütüphanesi kullandım hem RabbitMq kullanmak zorunluluğunu kaldırıyor, İstenirse Kafka gibi diğer message broker ları kullanabiliyoruz. Hemde Bir Local Message Table desteği var. Message broker üzerine gelen istekleri bir veritabanı aracılığıyla saklar. Bu sayede veri kayıplarını da engellemiş olur. Expire olmamış istekleri tekrar yollar.


