DROP TABLE JsonMappingConfiguration


CREATE TABLE JsonMappingConfiguration(
MappingID nvarchar(15) NOT NULL,
ConfigString varchar(max) NULL
)

INSERT INTO JsonMappingConfiguration VALUES ('TEST', 'AFAFAFAFFAFAF')