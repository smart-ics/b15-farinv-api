CREATE TABLE FARIN_StokLayerMap(
    StokLayerId VARCHAR(26) NOT NULL CONSTRAINT DF_FARIN_StokLayerMap_StokLayerId DEFAULT(''),
    StokBukuId VARCHAR(10) NOT NULL CONSTRAINT DF_FARIN_StokLayerMap_StokBukuId DEFAULT(''),
    
    CONSTRAINT PK_FARIN_StokLayerMap PRIMARY KEY CLUSTERED (StokBukuId)
)
GO

CREATE INDEX IX_FARIN_StokLayerMap_StokLayerId 
    ON FARIN_StokLayerMap(StokLayerId, StokBukuId);
GO