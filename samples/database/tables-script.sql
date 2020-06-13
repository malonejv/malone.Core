CREATE TABLE Todolists (
  Id        number not null,
  Name      varchar(100) default null,
  IsDeleted number(1) not null
  constraint pk_todoLists primary key (Id)
);

CREATE TABLE TaskItems (
  Id          number not null,
  Description varchar(100) not null,
  IsDeleted   number(1) not null
  Todolist_Id number not null,
  constraint pk_taskItems primary key (Id),
  constraint fk_taskItems_todoLists foreing key(Todolist_Id) references Todolists
);
