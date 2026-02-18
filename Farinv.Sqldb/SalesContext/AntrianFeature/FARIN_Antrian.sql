CREATE TABLE FARIN_Antrian
(
	AntrianId           VARCHAR(26) NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianId DEFAULT(''),
    AntrianDate         DATETIME    NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianDate DEFAULT('3000-01-01'),
    StartTime           VARCHAR(5)  NOT NULL CONSTRAINT DF_FARIN_Antrian_StartTime DEFAULT('00:00'),
    EndTime             VARCHAR(5)  NOT NULL CONSTRAINT DF_FARIN_Antrian_EndTime DEFAULT('00:00'),   
    ServicePoint        INT         NOT NULL CONSTRAINT DF_FARIN_Antrian_ServicePoint DEFAULT(0),
    SequenceTag         VARCHAR(16) NOT NULL CONSTRAINT DF_FARIN_Antrian_SequenceTag DEFAULT(''),
    AntrianDescription  VARCHAR(32) NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianDesc DEFAULT(''),

    CONSTRAINT PK_FARIN_Antrian PRIMARY KEY CLUSTERED (AntrianId),
    CONSTRAINT UQ_Antrian_UniqueEntry UNIQUE (AntrianDate, StartTime, ServicePoint)
)
GO
