CREATE TABLE "LocalizationRecords" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_DataEventRecord" PRIMARY KEY AUTOINCREMENT,
    "Key" TEXT,
    "ResourceKey" TEXT,
    "Text" TEXT,
    "LocalizationCulture" TEXT,
    "UpdatedTimestamp" TEXT NOT NULL
)

CREATE TABLE "ExportHistoryDbSet" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ExportHistoryDbSet" PRIMARY KEY AUTOINCREMENT,
    "Exported" TEXT NOT NULL,
    "Reason" TEXT
)

CREATE TABLE "ImportHistoryDbSet" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ImportHistoryDbSet" PRIMARY KEY AUTOINCREMENT,
    "Imported" TEXT NOT NULL,
    "Information" TEXT
)