CREATE TABLE PT_TABLES_DICT(	
	TABLE_NAME nvarchar(100) NULL,
        MS nvarchar(100) NULL,
        exportflag nvarchar(10) NULL
);
--drop table PT_CHAPTER_DICT
CREATE TABLE PT_CHAPTER_DICT(	
	TABLE_NAME nvarchar(100) not NULL,
        CHAPTER_NAME nvarchar(4000) not NULL,
        DATA_DETAIL nvarchar(500) NULL,
        CLASS nvarchar(10) NULL
);

--alter table PT_CHAPTER_DICT add constraint PK_CHAPTER_ID primary key (CHAPTER_NAME, TABLE_NAME);

CREATE TABLE PT_COMPARISON(
  FIELD_NAME nvarchar(100) NULL,
 LOCAL_VALUE nvarchar(100) NULL,
 TARGET_VALUE  nvarchar(100) NULL 
) ;
drop table PT_RESTORE
CREATE TABLE PT_RESTORE(
  OBJECT_NAME nvarchar(100)not NULL,
  PATIENT_ID nvarchar(100)not NULL,
  VISIT_ID nvarchar(100)not NULL,
  LOG_DATE  nvarchar(100) NULL 
) ;

alter table PT_RESTORE add constraint PK_RESTORE primary key (OBJECT_NAME, PATIENT_ID, VISIT_ID);


