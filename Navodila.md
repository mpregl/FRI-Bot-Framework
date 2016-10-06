# Delavnica Bot Framework #

## Cilji: ##
- Delujoč chat bot, ki zna interpetirati ukaze, jih sprocesirati ter vrniti odgovor
- integracija v Skype/Slack/FB
- Procesiranje jezika


## Naloge ##

1. **Osnove - postavitev okolja** (
	1. VS + updates
	2. Bot Channel Emulator
	3. Bot Template
	4. Azure subscription

2. **Bot framework **
	1. Signup
	2. Ustvarite bot (Skype)
	3. Ustvarite Azure App ID
		
3.	**BotBuilder**
	1.	copy zip https://github.com/Microsoft/BotBuilder
	2.	oglej Samples

			
4. **Sample bot 1** 
	1. New project
	2. App id and Bot id
	3. prvi odgovor
	4. Test z Bot Channel Emulatorjem
	3. Publish na Azure
	4. test preko Skype-a
		
5.	**Luis **
	1.	Add 2 intents, 5 tags each
		1.	+ None
		2.	+ hello
	3.	Api-ji
	4.	Json results
	5.	Publish/train
	6.	Tricks
	7.	Export Luis

6.	**Luis project 1**
	1.	Dodaj novi projekt v isti solution
	2.	Nastavi IIS Express
	2.	Task - list intents from session
	2.	Nastavi property IIS
		
7.	**FormFlow**
	1.	pripravite json datoteko z koraki
	2.	procesiranje rezultatov in izpis korakov
	3.	Ne pozabi na build action - resource	
		
8)	**Luis project 2**
	1.	nov class, ki extenda LuisDialog
	2.	Luis modell in intenti
	3.	Vsaj 2 intenta in default odgovor
		1.	V enem poklicati en web service
		2.	V drugem dodati noter counter in vračat, koliko klicev smo že naredili
