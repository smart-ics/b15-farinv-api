CREATE TABLE FARIN_Antrian
(
	AntrianId       VARCHAR(26)     NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianId DEFAULT(''),
    AntrianDate     DATETIME        NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianDate DEFAULT('3000-01-01'),
    SequenceTag     VARCHAR(10)     NOT NULL CONSTRAINT DF_FARIN_Antrian_SequenceTag DEFAULT(''),
    NoAntrian       INT             NOT NULL CONSTRAINT DF_FARIN_Antrian_NoAntrian DEFAULT(0),
    AntrianStatus   INT             NOT NULL CONSTRAINT DF_FARIN_Antrian_AntrianStatus DEFAULT(0),
    PersonName      VARCHAR(40)     NOT NULL CONSTRAINT DF_FARIN_Antrian_PersonName DEFAULT(''),
    
    CrtUser          VARCHAR(50)    NOT NULL CONSTRAINT DF_FARIN_Antrian_CrtUser DEFAULT(''),
    CrtDate          DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_CrtDate DEFAULT('3000-01-01'),
    UpdUser          VARCHAR(50)    NOT NULL CONSTRAINT DF_FARIN_Antrian_UpdUser DEFAULT(''),
    UpdDate          DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_UpdDate DEFAULT('3000-01-01'),
    VodUser          VARCHAR(50)    NOT NULL CONSTRAINT DF_FARIN_Antrian_VodUser DEFAULT(''),
    VodDate          DATETIME       NOT NULL CONSTRAINT DF_FARIN_Antrian_VodDate DEFAULT('3000-01-01'),

    CONSTRAINT PK_FARIN_Antrian PRIMARY KEY CLUSTERED (AntrianId, NoAntrian)    
)
