# ProyectoGIS
Base de datos :

create table SUCURSAL (
   idbranch             SERIAL     primary key     not null,
   name_branch          VARCHAR(150)          null,
   addres_branch       VARCHAR(150)          null,
   aforo     INT4                 null,
   geom Geometry(POINT,4326),
   latitud float,
   longitud float
   
);
