### 1. Beskriv vad lastbalansering innebär (i allmänhet, behöver inte gälla just databaser)

Innebär att man kan dela upp arbete / trafik mellan flera resurser, för att undvika överbelastning.

### 2. Ge ett konkret exempel på där lastbalansering kan vara bra att använda.

En hemsida kan använda sig av cache för att spara frekventa queries, vilket minskar belastningen på databasen.

### 3. Anta att du bygger en hemsida. Vilka fördelar finns med använda lastbalansering? Ange tre fördelar

Kortare svarstider, mindre risk för överbelastning, bättre tillgänglighet.

### 4. Vad är SQL Server Replication?

Replikation innebär att man kopierar och distribuerar databas-objekt från en databas till en annan och sedan synkroniserar databaserna. Alla kopior är aktiva.

### 5. Vad är Mirroring i SQL Server?

Man skapar en kopia för backup som inte är aktiv. Går inte att läsa / skriva till. Information går bara till kopian.

### 6. Vad är AlwaysOn Availability Groups i SQL Server?

En funktion för hög tillgänglighet. Har kopior av databaser på flera servrar så om någon går ner så tar en anna över.
