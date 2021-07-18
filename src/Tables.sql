DROP TABLE JsonMappingConfiguration
GO

CREATE TABLE JsonMappingConfiguration(
MappingID nvarchar(15) NOT NULL,
GraphName	varchar(100) NOT NULL DEFAULT '',
ConfigString varchar(max) NULL
)
GO

INSERT INTO JsonMappingConfiguration VALUES ('TEST', 'PX.Objects.SO.SOOrderEntry', 'AFAFAFAFFAFAF')
GO

DROP TABLE IntegrationScenario


CREATE TABLE IntegrationScenario(
ScenarioID nvarchar(15) NOT NULL,
LastScenarioLineNbr	int NOT NULL DEFAULT 0
)
GO

DROP TABLE IntegrationScenarioDetail
GO

CREATE TABLE IntegrationScenarioDetail(
ScenarioID nvarchar(15) NOT NULL,
LineNbr int NOT NULL,
SortOrder int NOT NULL,
Mapping nvarchar(15) NOT NULL,
Direction varchar(3) NOT NULL,
Webhook uniqueidentifier null,
Url nvarchar(256) NULL,
StatusCode varchar(3) NULL
)
GO
