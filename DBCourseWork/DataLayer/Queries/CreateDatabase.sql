create database db_course_work

create table users (
	id int identity(1, 1),
	[name] nchar(30),
	constraint PK_USERS primary key (id)
)
go

create unique nonclustered index users_name_index on users ([name])
go

create table groups (
	id int identity(1, 1),
	[name] nchar(30),
	constraint PK_GROUPS primary key (id)
)


create table user_groups (
	[user_id] int,
	group_id int,
	constraint PK_USER_GROUPS primary key ([user_id], group_id),
	constraint FK_USER_GROUPS_USERS foreign key ([user_id])
		references users (id)
		on update cascade
		on delete cascade,
	constraint FK_USER_GROUPS_GROUPS foreign key (group_id)
		references groups (id)
		on update cascade
		on delete cascade
)

create table expenses_header (
	id int identity(1, 1),
	[description] nchar(50),
	[time] smalldatetime,
	group_id int,
	[user_id] int,
	constraint PK_EXPENSES_HEADER primary key (id),
	constraint FK_EXPENSES_HEADER_GROUPS foreign key (group_id)
		references groups (id)
		on update cascade
		on delete cascade,
	constraint FK_EXPENSES_HEADER_USERS foreign key ([user_id])
		references users (id)
		on update cascade
		on delete cascade
)

create table expenses_line (
	[user_id] int,
	amount money,
	expense_id int,
	constraint PK_EXPENSES_LINE primary key ([user_id], expense_id),
	constraint FK_EXPENSES_LINE_USERS foreign key ([user_id])
		references users (id),
	constraint FK_EXPENSES_LINE_EXPENSES_HEADER foreign key (expense_id)
		references expenses_header (id)
		on update cascade
		on delete cascade
)


create table payments (
	id int identity(1, 1),
	[description] nchar(50),
	[time] smalldatetime,
	group_id int,
	[user_id] int,
	to_user_id int,
	amount money,
	confirmed bit,
	constraint PK_PAYMENTS primary key (id),
	constraint FK_PAYMENTS_GROUPS foreign key (group_id)
		references groups (id)
		on update cascade
		on delete cascade,
	constraint FK_PAYMENTS_USERS foreign key ([user_id])
		references users (id)
		on update cascade
		on delete cascade,
	constraint FK_PAYMENTS_TO_USERS foreign key (to_user_id)
		references users (id)
)