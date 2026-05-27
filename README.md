# 💰 CounterApp

![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MAUI](https://img.shields.io/badge/MAUI-Cross--Platform-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

**CounterApp** è un'applicazione multipiattaforma sviluppata con il framework **.NET 7** e **.NET MAUI** (Multi-platform App UI). L'obiettivo principale dell'applicazione è consentire all'utente di gestire, monitorare e tracciare in modo semplice e intuitivo tutti i propri movimenti finanziari, fornendo un controllo completo sulle proprie finanze personali da un'unica interfaccia condivisa tra più dispositivi.

---

## ✨ Funzionalità Principali

* **📊 Gestione Completa delle Transazioni:** Registrazione rapida e dettagliata di entrate e uscite finanziarie.
* **💰 Monitoraggio del Saldo:** Visualizzazione in tempo reale del bilancio attuale, con calcoli dinamici basati sullo storico dei movimenti.
* **📱 Supporto Multipiattaforma:** Grazie alle potenzialità di .NET MAUI, il progetto utilizza un'unica base di codice C# per essere distribuito nativamente su Android, iOS, macOS (Mac Catalyst) e Windows (WinUI 3).
* **🎨 Interfaccia Moderna ed Intuitiva:** Design reattivo pensato per adattarsi perfettamente a schermi di diverse dimensioni (smartphone, tablet, desktop).

---

## 📱 Dettagli sul Funzionamento (Workflow Utente)

L'applicazione è progettata per offrire un'esperienza utente fluida e immediata, basata sui seguenti flussi operativi:

1. **Dashboard di Riepilogo:** All'avvio, l'utente viene accolto da una schermata principale (Home) che mostra il **saldo totale aggiornato**. Grafiche e indicatori visivi permettono di capire a colpo d'occhio il rapporto tra entrate e uscite del periodo corrente.
   
2. **Inserimento di un Nuovo Movimento:**
   Attraverso un'interfaccia dedicata e di facile accesso (es. un Floating Action Button), l'utente può registrare una nuova transazione. Il modulo richiede informazioni chiave come:
   * **Tipologia:** Entrata (es. stipendio, regali) o Uscita (es. spesa, bollette, svago).
   * **Importo:** Il valore numerico della transazione.
   * **Data:** Impostata di default a quella odierna, ma modificabile per inserimenti retroattivi.
   * **Descrizione/Categoria:** Brevi note per identificare facilmente il movimento in futuro.

3. **Storico e Consultazione:**
   L'app dispone di una sezione dedicata allo **storico completo** delle transazioni. L'utente può scorrere l'elenco cronologico di tutti i movimenti registrati, con la possibilità di verificare i dettagli di ogni singola voce. 

4. **Gestione Locale dei Dati (Offline-First):**
   I dati finanziari vengono salvati in locale sul dispositivo dell'utente. Questo garantisce che l'applicazione sia reattiva, che tuteli la privacy (nessun dato sensibile inviato a server esterni non autorizzati) e che possa funzionare perfettamente anche in assenza di connessione internet.

---

## 🛠️ Architettura e Tecnologie Utilizzate

* **Linguaggio di Programmazione:** C# (100% della codebase).
* **Framework Base:** .NET 7.0.
* **Framework UI:** .NET MAUI.
* **Pattern Architetturale:** Strutturazione basata su **MVVM** (Model-View-ViewModel) per una netta separazione tra la logica di business e l'interfaccia utente.

---

## 🚀 Requisiti di Sistema e Prerequisiti

Per compilare, eseguire e modificare il progetto in ambiente di sviluppo, è necessario soddisfare i seguenti requisiti:

1.  **IDE:** Visual Studio 2022 (versione 17.3 o superiore) con il carico di lavoro **"Sviluppo di app .NET MAUI"** installato.
2.  **SDK:** .NET 7.0 SDK.
3.  **Ambienti di Test:** Emulatore Android o dispositivo fisico per i test mobile; Windows Machine per i test desktop.

---

## ⚙️ Installazione e Avvio Rapido

1.  **Clona la repository:**
    ```
bash
    git clone [https://github.com/GBABB1806/CounterApp.git](https://github.com/GBABB1806/CounterApp.git)
    ```
2.  **Apri la Soluzione:** Apri `CounterApp.sln` nel tuo IDE.
3.  **Ripristina i pacchetti NuGet** se richiesto dall'IDE.
4.  **Seleziona la piattaforma target** e clicca su **Avvia** (`F5`).

---

## 📂 Struttura della Repository

* `/CounterApp` - Codice sorgente dell'applicazione MAUI (Views, ViewModels, Models).
* `CounterApp.sln` - File di soluzione di Visual Studio.
* `.gitattributes` & `.gitignore` - Configurazioni di Git.

---

## 🤝 Come Contribuire

I contributi sono molto apprezzati!
1. Fai un **Fork** del progetto.
2. Crea un branch (`git checkout -b feature/NuovaFunzione`).
3. Fai il commit (`git commit -m 'Aggiunta nuova funzione'`).
4. Fai il push (`git push origin feature/NuovaFunzione`).
5. Apri una **Pull Request**.

---

*Sviluppato da [GBABB1806](https://github.com/GBABB1806).*
"""
