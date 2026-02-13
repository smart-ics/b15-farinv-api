CREATE TABLE FARIN_Antrian
(
	AntrianId       VARCHAR(26)     NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianId DEFAULT(''),
    --AntrianDate     DATETIME        NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianDate DEFAULT('3000-01-01'),
    --SequenceTag     VARCHAR(10)     NOT NULL CONSTRAINT DF_FARIN_Antrian_SequenceTag DEFAULT(''),
    --NoAntrian       INT             NOT NULL CONSTRAINT DF_FARIN_Antrian_NoAntrian DEFAULT(0),
    --AntrianStatus   INT             NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianStatus DEFAULT(0),
    --PersonName      VARCHAR(40)     NOT NULL CONSTRAINT DF_FARIN_Antrian_PersonName DEFAULT(''),
    
    --TakenAt         DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_TakenAt DEFAULT('3000-01-01'),
    --AssignedAt      DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_AssignedAt DEFAULT('3000-01-01'),
    --PreparedAt      DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_PreparedAt DEFAULT('3000-01-01'),
    --DeliveredAt     DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_DeliveredAt DEFAULT('3000-01-01'),
    --CancelAt        DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_CancelAt DEFAULT('3000-01-01'),

    --CONSTRAINT PK_FARIN_Antrian PRIMARY KEY NONCLUSTERED (AntrianId)    
)
GO

--CREATE CLUSTERED INDEX CX_FARIN_Antrian_DateNo ON FARIN_Antrian (AntrianDate, NoAntrian)
--GO

--CREATE NONCLUSTERED INDEX IX_FARIN_Antrian_AntrianDate ON FARIN_Antrian (AntrianDate)
--GO
