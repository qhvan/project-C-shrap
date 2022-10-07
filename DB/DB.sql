drop database if exists MSHOP;
create database MSHOP;
use MSHOP;

create table if not exists customer(
	customer_id int auto_increment primary key,
    customer_name varchar(45) not null,
    customer_phone varchar (10) not null,
    customer_address varchar(50) not null
);

create table if not exists adminS (
admin_id int auto_increment primary key,
admin_user varchar(30) ,
admin_pass varchar(8) ,
admin_name varchar(50) not null,
checkA int not null default 1
);
insert into adminS(admin_user,admin_name, admin_pass)
values ('qhv','', '123'),
('dung','','111'),
('lqt','','222');

create table if not exists user1(
	user_id int primary key auto_increment,
    customer_id int not null,
    name_user varchar(45) not null,
    account_user varchar(50) not null,
    password_user varchar (10) not null
);

create table if not exists staff(
	staff_id int primary key auto_increment,
    staff_name varchar(45) not null,
    staff_phone varchar(10) not null unique,
    staff_address  varchar(100),
    staff_role int not null default 1, -- 1. staff
    staff_username varchar(45) not null unique,
    staff_password varchar(45) not null
);

insert into staff(staff_name, staff_username, staff_password, staff_phone, staff_address, staff_role)
values('Quach Huu Van', 'qhvtest', 'van123', '0988721017', 'Hà Nội', 1);


create table if not exists category(
	category_id int auto_increment primary key,
    category_name varchar(45) not null
);
insert into category(category_name)
values ('Bánh'),
('Kẹo'),
('Nước ngọt'),
('Trà');

create table if not exists product(
	product_id int auto_increment primary key,
    category_id int not null,
    product_name varchar(50) not null,
	product_price decimal not null,
    product_description varchar(200) not null,
    product_quantity int not null,
    foreign key (category_id) references category(category_id)
);

insert into product(product_id, category_id, product_name, product_price, product_description, product_quantity)
value('1','1','Bánh Quy','42000','đặc biệt' , ' 200 '),
('2','2','Kẹo mút','1000','nhiều vị', '200' ),
('3','3','Cocca','10000','không đường', '200' ),
('4','4','Trà Loại 1','300000','đậm đà', '200' ),
('5','3','Pessi','12000','mới lạ', '200' ),
('6','3','Fanta','8000','chai sành', '200' ),
('7','1','Bánh KFC ','30000','vị rong biển', '200' ),
('8','1','Bánh Socola','200000','mới lạ, hấp dẫn', '200' ),
('9','4','Trà Sâm','400000','tốt cho sức khỏe', '200' ),
('10','4','Trà Thái Nguyên','300000','đặc sản', '200' ),
('11','2','Kẹo sữa','3000','ngọt ngào', '200' ),
('12','2','Kẹo ngô','300000','vị mới lạ', '200' ),
('13','4','Trà Sen','350000','thơm dịu', '200' );

create table orders(
	order_id int auto_increment primary key,
    staff_id int not null,
    order_date datetime default current_timestamp() not null,
    order_status int not null, -- 1: Create New Order
    foreign key (staff_id) references staff(staff_id)
 --    foreign key (customer_id) references customer(customer_id)-- 
);


create table order_details(
	order_id int not null,
    product_id int not null,
    quantity int not null,
    unit_price decimal not null,	
    primary key (order_id, product_id),
    foreign key (order_id) references orders(order_id),
    foreign key (product_id) references product(product_id)
);

select * from customer;
select * from adminS;
select * from staff;
select * from user1;
select * from category;
select * from product;
select * from order_details;
select * from orders;

SET GLOBAL FOREIGN_KEY_CHECKS=0;

drop table customer;
drop table adminS;
drop table staff;
drop table user1;
drop table category;
drop table order_details;
drop table orders;
drop table product;

