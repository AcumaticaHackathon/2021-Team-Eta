DROP TABLE JsonMappingConfiguration


CREATE TABLE JsonMappingConfiguration(
MappingID nvarchar(15) NOT NULL,
GraphName	varchar(100) NOT NULL DEFAULT '',
ConfigString varchar(max) NULL
)

INSERT INTO JsonMappingConfiguration VALUES ('TEST', 'PX.Objects.SO.SOOrderEntry', 'AFAFAFAFFAFAF')