CREATE TABLE "LocalizationRecords" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_DataEventRecord" PRIMARY KEY AUTOINCREMENT,
    "Key" TEXT,
	"ResourceKey" TEXT,
    "Text" TEXT,
    "LocalizationCulture" TEXT,
    "UpdatedTimestamp" TEXT NOT NULL
)