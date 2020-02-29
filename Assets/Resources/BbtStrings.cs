using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BbtStrings
{

    public static int language = 0;

    public static string GetStr( string bbt )
    {
        if ( STR.ContainsKey(bbt) )
        {
            return STR[bbt][language];
        }

        return bbt;
    }

    //0 - рус, 1 - англ
    public static Dictionary<string, string[]> STR = new Dictionary<string, string[]>()
    {
        //inventory names
         { "str_inv_cure",            new string[]{ "Лекарство",            "Cure" } }
        ,{ "str_inv_bandage",         new string[]{ "Бинт",                 "Bandage" } }
        ,{ "str_inv_antibiotic",      new string[]{ "Антибиотик",           "Antibiotic" } }
        ,{ "str_inv_friedmushrooms",  new string[]{ "Жареные грибы",        "Fried mushrooms" } }
        ,{ "str_inv_energybar",       new string[]{ "Батончик",             "Energy bar" } }
        ,{ "str_inv_noodles",         new string[]{ "Доширак",              "Noodles" } }
        ,{ "str_inv_rat",             new string[]{ "Крыса",                "Rat" } }
        ,{ "str_inv_mushrooms",       new string[]{ "Грибы",                "Mushrooms" } }
        ,{ "str_inv_water",           new string[]{ "Вода",                 "Water" } }
        ,{ "str_inv_coffee",          new string[]{ "Кофе",                 "Coffee" } }
        ,{ "str_inv_juice",           new string[]{ "Сок",                  "Juice" } }
        ,{ "str_inv_cola",            new string[]{ "Кола",                 "Cola" } }
        ,{ "str_inv_axe",             new string[]{ "Топор",                "Axe" } }
        ,{ "str_inv_stuff",           new string[]{ "Компоненты",           "Components" } }
        ,{ "str_inv_money",           new string[]{ "Деньги",               "Money" } }
        ,{ "str_inv_humus",           new string[]{ "Перегной",             "Humus" } }
        ,{ "str_inv_friedrat",        new string[]{ "Жареная крыса",        "Fried rat" } }
        ,{ "str_inv_computer",        new string[]{ "Компьютер",            "Computer" } }
        ,{ "str_inv_crafttable",      new string[]{ "Крафтовый стол",       "Craft table" } }
        ,{ "str_inv_wires",           new string[]{ "Провода",              "Wires" } }
        ,{ "str_inv_fir",             new string[]{ "Елочка",               "Christmas tree" } }
        ,{ "str_inv_key1",            new string[]{ "Ключ 1 этаж",          "Floor key 1" } }
        ,{ "str_inv_key2",            new string[]{ "Ключ 2 этаж",          "Floor key 2" } }
        ,{ "str_inv_condey",          new string[]{ "Кондиционер",          "Сonditioner" } }
        ,{ "str_inv_corob",           new string[]{ "Коробки",              "Boxes" } }
        ,{ "str_inv_key4",            new string[]{ "Ключ 4 этаж",          "Floor key 4" } }
        ,{ "str_inv_key5",            new string[]{ "Ключ 5 этаж",          "Floor key 5" } }
        ,{ "str_inv_kreslo",          new string[]{ "Кресло",               "Chair" } }
        ,{ "str_inv_sofa",            new string[]{ "Диван",                "Sofa" } }
        ,{ "str_inv_monitor",         new string[]{ "Монитор",              "Monitor" } }
        ,{ "str_inv_mushtrap",        new string[]{ "Грибной сад",          "Mushroom bed" } }
        ,{ "str_inv_rattrap",         new string[]{ "Крысиная ловушка",     "Rat trap" } }
        ,{ "str_inv_watertrap",       new string[]{ "Водяной фильтр",       "Water filter" } }
        ,{ "str_inv_wood",            new string[]{ "Дерево",               "Wood" } }
        ,{ "str_inv_zapchasti",       new string[]{ "Запчасти",             "Spares" } }

        //параметры
        ,{ "str_param_0",                       new string[]{ "Жажда",                  "Thirst" } }
        ,{ "str_param_1",                       new string[]{ "Сытость",                "Hunger" } }
        ,{ "str_param_2",                       new string[]{ "Энергия",                "Energy" } }
        ,{ "str_param_3",                       new string[]{ "Здоровье",               "Health" } }
        ,{ "str_desease_0",                     new string[]{ "Рана",                   "Wound" } }
        ,{ "str_desease_1",                     new string[]{ "Болезнь",                "Desease" } }
        ,{ "str_project",                       new string[]{ "Проект",                 "Project" } }

        //inventory description не переведено
        ,{ "str_inv_cure_desc",                 new string[]{ "Средство от легкой болезни",                                             "Cure" } }
        ,{ "str_inv_bandage_desc",              new string[]{ "Средство от физических ранений",                                         "Bandage" } }
        ,{ "str_inv_antibiotic_desc",           new string[]{ "Средство от тяжелой формы болезни",                                      "Antibiotic" } }
        ,{ "str_inv_friedmushrooms_desc",       new string[]{ "Вкусные жареные грибочки",                                               "Soupe" } }
        ,{ "str_inv_energybar_desc",            new string[]{ "Энергетический батончик не только восполняет сытость, но и энергию",     "Energybar" } }
        ,{ "str_inv_noodles_desc",              new string[]{ "Питательная лапша быстрого приготовления",                               "Noodles" } }
        ,{ "str_inv_rat_desc",                  new string[]{ "Не считая дивана, единственное мясо в этом офисе",                       "Rat" } }
        ,{ "str_inv_mushrooms_desc",            new string[]{ "Нету суши, нету пиццы, ешьте вы пучки грибицы",                         "Mushrooms" } }
        ,{ "str_inv_water_desc",                new string[]{ "Чистейшая вода",                                                         "Water" } }
        ,{ "str_inv_coffee_desc",               new string[]{ "Топливо для программиста",                                               "Coffee" } }
        ,{ "str_inv_juice_desc",                new string[]{ "Кто додумался давить сок из чеснока?",                                   "Juice" } }
        ,{ "str_inv_cola_desc",                 new string[]{ "Не употреблять вместе с кофе!",                                          "Cola" } }
        ,{ "str_inv_axe_desc",                  new string[]{ "Убойный инструмент против босса",                                        "Axe" } }
        ,{ "str_inv_stuff_desc",                new string[]{ "Из гвоздя, скрепки и бумажки сделаю спутниковое ТВ",                     "Stuff" } }
        ,{ "str_inv_money_desc",                new string[]{ "Программисту заплатите чеканной... Кхм",                                 "Money" } }
        ,{ "str_inv_humus_desc",                new string[]{ "Сила в перегное!",                                                       "Humus" } }
        ,{ "str_inv_friedrat_desc",             new string[]{ "С хрустящим хвостиком!", "With a crispy ponytail!" } }
        ,{ "str_inv_computer_desc",             new string[]{ "Играть в рабочее время станет намного веселее", "Play during working hours will be much more fun" } }
        ,{ "str_inv_crafttable_desc",           new string[]{ "Поможет быстрее мастерить предметы", "It will help to craft items faster." } }
        ,{ "str_inv_wires_desc",                new string[]{ "Проводимость проверена языком", "Conductivity checked by licking" } }
        ,{ "str_inv_fir_desc",                  new string[]{ "Главная красавица. Помогает быстрее работать.", "The main beauty. Helps to work faster." } }
        ,{ "str_inv_key1_desc",                 new string[]{ "Электронный ключ от 1 этажа", "Electronic key from the 1st floor" } }
        ,{ "str_inv_key2_desc",                 new string[]{ "Электронный ключ от 2 этажа", "Electronic key from the 2nd floor" } }
        ,{ "str_inv_condey_desc",               new string[]{ "Повышает эффективность лени", "Increases the effectiveness of laziness" } }
        ,{ "str_inv_corob_desc",                new string[]{ "Домик котиков :З", "Feline house :3" } }
        ,{ "str_inv_key4_desc",                 new string[]{ "Электронный ключ от 4 этажа", "Electronic key from the 4th floor" } }
        ,{ "str_inv_key5_desc",                 new string[]{ "Электронный ключ от 5 этажа", "Electronic key from the 5th floor" } }
        ,{ "str_inv_kreslo_desc",               new string[]{ "Перевернутая табуретка и то удобнее", "Inverted stool and it’s more convenient" } }
        ,{ "str_inv_sofa_desc",                 new string[]{ "Самое посещаемое место на работе", "Most visited place at work" } }
        ,{ "str_inv_monitor_desc",              new string[]{ "Настоящий! Даже с подсветкой!", "Cool! Even with backlight!" } }
        ,{ "str_inv_mushtrap_desc",             new string[]{ "Здесь можно выращивать грибочки. Бугагашенька!", "Here you can grow mushrooms. Bazinga!" } }
        ,{ "str_inv_rattrap_desc",              new string[]{ "Гуманная ловушка для крыс", "Humane Rat Trap" } }
        ,{ "str_inv_watertrap_desc",            new string[]{ "Помогает фильтровать воду из самых неприятных мест", "Helps filter water from the most unpleasant places" } }
        ,{ "str_inv_wood_desc",                 new string[]{ "Ну дерево и дерево. Что бубнить то?", "Well tree and tree. What to mutter then?" } }
        ,{ "str_inv_zapchasti_desc",            new string[]{ "Лишние детали после ремонта любой техники", "Extra parts after repair of any equipment" } }


        //фразы торговца
        ,{ "str_start_trade",                   new string[]{ "Что предложите?", "What is there for sale?" } }
        ,{ "str_not_good_trade",                new string[]{ "Так не пойдет. Давай больше.", "Will not work. Come on more." } }
        ,{ "str_good_trade_1",                  new string[]{ "Думаю это справедливо.", "I think it's fair" } }
        ,{ "str_good_trade_2",                  new string[]{ "Отличное предложение!", "Great offer!" } }
        ,{ "str_good_trade_3",                  new string[]{ "Какая щедрость!", "What a generosity!" } }

        //фразы состояния
        ,{ "thinks",               new string[]{ "Мысли", "Thoughts" } }
        ,{ "str_status_player_0",               new string[]{ "Моя лень меня бережет!", "My laziness protects me!" } }
        ,{ "str_status_player_1",               new string[]{ "От голода скоро начну грызть стулья", "From hunger I will begin to gnaw on chairs." } }
        ,{ "str_status_player_2",               new string[]{ "Ради еды готов прогрызть выход наружу.", "For food, ready to gnaw a way out." } }
        ,{ "str_status_player_3",               new string[]{ "Только что во рту почувствовал движение перекати-поля.", "I just felt a tumbleweed in my mouth." } }
        ,{ "str_status_player_4",               new string[]{ "Может создать замкнутую цепь круговорота жидкости в организме?", "Can create a closed circuit of the fluid cycle in the body?" } }
        ,{ "str_status_player_5",               new string[]{ "Обыскал весь офис. Не нашел ни одного подорожника.", "Searched the entire office. I did not find a single plantain." } }
        ,{ "str_status_player_6",               new string[]{ "Против любой болезни поможет клизма!", "An enema helps against any disease!" } }
        ,{ "str_status_player_7",               new string[]{ "Я доделал проект! О нет. Мне это приснилось.", "I completed the project! Oh no. I dreamed it." } }
        ,{ "str_status_player_8",               new string[]{ "Подушка - мой лучший друг.", "Pillow is my best friend." } }

        //фразы рандомных событий
        ,{ "str_random_event_0",                new string[]{ "Уснул на клавиатуре. Удалил половину проекта. Удалено 3% проекта", "I fell asleep on the keyboard. Deleted half of the project. Removed 3% of the projectю" } }
        ,{ "str_random_event_1",                new string[]{ "Улыбнулся пробегающей крысе. Та упала навзничь. Получена крыса.", "He smiled at the running rat. She fell back. Got a rat." } }
        ,{ "str_random_event_2",                new string[]{ "Над головой загорелась лампочка. Упала и разбилась. Проект продвинулся, но стекла остались на лице. Добавлено 2% проекта. Получена травма", "A light came on above his head. Fell and crashed. The project advanced, but the glass remained on the face. Added 2% of the project. Received an injury." } }
        ,{ "str_random_event_3",                new string[]{ "Ночью дедушка Мороз батончик и колу мне принес. Оливье бы. Получены батончик и кола.", "At night, Santa Claus brought me a bar and Coke. Olivier would. Received a bar and a stake." } }
        ,{ "str_random_event_4",                new string[]{ "Для ускорения работы вы попытались дополнительно работать ногами на клавиаутуре. Она не выдержала запах ваших ног. Пришлось потратить не мало времени, чтобы уговорить ее работать. Потрачена энергия.", "To speed up the work, you tried to additionally work with your feet on the keyboard. She could not stand the smell of your legs. I had to spend a lot of time to persuade her to work. Energy spent." } }
        ,{ "str_random_event_5",                new string[]{ "Не стоило жевать найденную шоколадку за компом в пыльном помещении. Мой чих был слышен на другом конце города. Я похож на пульверизатор. Потрачена энергия на чистку монитора.", "You should not chew the chocolate found at the computer in a dusty room. My sneeze was heard across the city. I look like a spray gun. Energy spent cleaning the monitor." } }
        ,{ "str_random_event_6",                new string[]{ "-Хммм, а что это ты жуешь?\n-Не знаю! Само приползло. \n Добавлена сытость.", "-Hmmm, what are you chewing?\n-I don’t know! It crawled itself.  \n Satiety added." } }
        ,{ "str_random_event_7",                new string[]{ "Нашел в вентиляции застрявшего коллегу, который пропал неделю назад. Его тоже оставляли закончить проект, но во время побега он застрял. Забрал его собранные припасы. Сейчас они мне нужнее. Добавлены батончик и сок.", "I found a stuck colleague in the ventilation who disappeared a week ago. He was also left to finish the project, but he got stuck during the escape. He took his collected supplies. Now I need them. Added bar and juice." } }
        ,{ "str_random_event_8",                new string[]{ "Мне снилось как крысы спускались на веревках, как шпионы, чтобы выкрасть кусочек еды. Я думал это сон, пока не обнаружил, что пропала пачка лапши.", "I dreamed of rats coming down on ropes, like spies, to steal a piece of food. I thought it was a dream until I discovered that a packet of noodles had disappeared." } }
        ,{ "str_random_event_9",                new string[]{ "Каждый веселится по разному в Новый Год. Кто то танцует. Кто то пускает салюты. А кто то запускает горящего поросенка в здания. Пришлось потратить немного воды, чтобы потушить бедолагу.", "Everyone has fun differently on New Year. Someone is dancing. Someone fires salutes. And someone starts a burning pig in the building. I had to spend some water to extinguish the poor fellow." } }
        ,{ "str_random_event_10",               new string[]{ "Решил поспать на один глаз. Выспался наполовину.", "I decided to sleep in one eye. He slept half." } }
        ,{ "str_random_event_11",               new string[]{ "Протек унитаз. Зато получил немного воды.", "Drip toilet. But got a little water." } }
        ,{ "str_random_event_12",               new string[]{ "Нашел старую обувь коллеги. Набрал немного грибов.", "Found old colleagues shoes. I scored a lot of mushrooms." } }
        ,{ "str_random_event_13",               new string[]{ "Нашел кошелек босса. Назначил себе премию.", "Found a boss wallet. I appointed myself a prize." } }

        //фразы ежедневного выбора
        ,{ "str_everyday_event_0",              new string[]{ "В окно на большой скорости влетел разовый поросенок. Оставил на черный день. Покормить?", "A one-time piglet flew into the window at high speed. Left for a rainy day. Feed?" } }
        ,{ "str_everyday_event_1",              new string[]{ "Решил поспать на клавиатуре. На ней мягче. Стоит ли?", "I decided to sleep on the keyboard. It is softer on it. Is it worth it?" } }
        ,{ "str_everyday_event_2",              new string[]{ "Нашел лапшу в стене. Попробовать съесть?", "Found noodles in the wall. Try to eat?" } }
        ,{ "str_everyday_event_3",              new string[]{ "Открыл дорогие сигары босса. Стоит ли закурить?", "He opened the boss's expensive cigars. Is it worth it to smoke?" } }
        ,{ "str_everyday_event_4",              new string[]{ "Есть возможность подсмотреть проект у коллеги. Сделать это?", "There is an opportunity to see the project with a colleague. Do it?" } }
        ,{ "str_everyday_event_5",              new string[]{ "Чувствую беспомощность. Крикнуть ли на весь офис просьбу о помощи?", "I feel helpless. Should I call for help throughout the office?" } }
        ,{ "str_everyday_event_6",              new string[]{ "Тараканы выстроились напротив меня и приготовились к атаке. Готовиться к обороне?", "Cockroaches lined in front of me and prepared to attack. Prepare for defense?" } }
        ,{ "str_everyday_event_7",              new string[]{ "Нашел неизвестный журнал с надписью ХХХ. Почитать?", "I found an unknown magazine with the inscription XXX. To read?" } }
        ,{ "str_everyday_event_8",              new string[]{ "Обнаружил сейф коллеги. Там может быть все что угодно. Открыть?", "Found a colleague's safe. Anything can be there. Open?" } }
        ,{ "str_everyday_event_9",              new string[]{ "За окном что то блестит. Лизнуть это?", "Something glistens outside. Lick it?" } }
        ,{ "str_everyday_event_10",             new string[]{ "Ко мне пришел коллега для обмена. Предлагает лапшу. Хочет лекарство", "A colleague came to me for an exchange. Offers noodles. He wants a cure." } }
        ,{ "str_everyday_event_11",             new string[]{ "Нашел дыру в стене. Посмотреть?", "Found a hole in the wall. See there?" } }
        ,{ "str_everyday_event_12",             new string[]{ "Решил слепить снеговика. Стоит ли?", "I decided to make a snowman. Is it worth it?" } }
        ,{ "str_everyday_event_13",             new string[]{ "Громко играл на электрогитаре. Разбить ее о стену?", "He played loudly on an electric guitar. Smash it against the wall?" } }

        //ответа да ежедневного выбора
        ,{ "str_everyday_event_yes_0",              new string[]{ "Снял с него какие-то тряпки. Пригодятся в качестве бинтов.", "He took off some rags from him. Useful as bandages." } }
        ,{ "str_everyday_event_yes_1",              new string[]{ "Во сне случайно напечатал часть проекта.", "In a dream, accidentally printed part of the project." } }
        ,{ "str_everyday_event_yes_2",              new string[]{ "Попробовал укусить. Оказалось это электрическая проводка.", "I tried to bite. It turned out this is the electrical wiring." } }
        ,{ "str_everyday_event_yes_3",              new string[]{ "Сработала система пожаротушения. Набрал немного воды.", "The fire extinguishing system worked. Got some water." } }
        ,{ "str_everyday_event_yes_4",              new string[]{ "Кто мог подумать, что его проект отличается от моего? Наделал много ошибок в своем.", "Who would have thought that his project was different from mine? Made a lot of mistakes in his." } }
        ,{ "str_everyday_event_yes_5",              new string[]{ "Тараканы из всех щелей принесли мне угощение. Теперь не смогу уснуть.", "Cockroaches from all the cracks brought me a treat. Now I can’t fall asleep." } }
        ,{ "str_everyday_event_yes_6",              new string[]{ "Огонь в глазах, храбрость в сердце. И я побежден в битве 'один на один'.", "Fire in the eyes, courage in the heart. And I am defeated in a one-on-one battle." } }
        ,{ "str_everyday_event_yes_7",              new string[]{ "Теперь долго не смогу уснуть.", "Now I can’t sleep for a long time." } }
        ,{ "str_everyday_event_yes_8",              new string[]{ "Ура! Склад!", "Hurrah! Warehouse!" } }
        ,{ "str_everyday_event_yes_9",              new string[]{ "Ну почему я везде сую свой язык? Теперь он примерз.", "Why am I popping my tongue everywhere? Now he is freezing." } }
        ,{ "str_everyday_event_yes_10",             new string[]{ "Поговорили. Обменялись. Все довольны.", "We talked. Exchanged. Everyone is happy." } }
        ,{ "str_everyday_event_yes_11",             new string[]{ "Ура! Заначка!", "Hurrah! Nest egg!" } }
        ,{ "str_everyday_event_yes_12",             new string[]{ "Потратил немало сил. Зато работать стало веселее.", "Spent a lot of effort. But the work has become more fun." } }
        ,{ "str_everyday_event_yes_13",             new string[]{ "Придется купить начальнику новую гитару.", "We'll have to buy a new guitar for the boss." } }

        //ответа нет ежедневного выбора
        ,{ "str_everyday_event_no_0",              new string[]{ "Потратил силы чтобы отогнать наглого поросенка от моей еды.", "I spent my strength to drive the arrogant pig from my food." } }
        ,{ "str_everyday_event_no_1",              new string[]{ "Как же хочется спать.", "How sleepy." } }
        ,{ "str_everyday_event_no_2",              new string[]{ "От обиды съел свою лапшу.", "Offended ate his noodles." } }
        ,{ "str_everyday_event_no_3",              new string[]{ "Правильно! Лучше буду больше кушать.", "Right! I'd rather eat more." } }
        ,{ "str_everyday_event_no_4",              new string[]{ "Хе-хе. Сам написал.", "Hehe. I wrote it myself." } }
        ,{ "str_everyday_event_no_5",              new string[]{ "Нет. Я умный человек. Лучше сохраню энергию.", "Not. I am a smart person. Better save energy." } }
        ,{ "str_everyday_event_no_6",              new string[]{ "Рейд тараканов прошел успешно.", "The cockroach raid was successful." } }
        ,{ "str_everyday_event_no_7",              new string[]{ "Неудовлетворенное любопытство вызвало жажду.", "Unsatisfied curiosity caused thirst." } }
        ,{ "str_everyday_event_no_8",              new string[]{ "Сказал об этом коллеге. Получил помощь в проекте.", "He told about this colleague. Got help in the project." } }
        ,{ "str_everyday_event_no_9",              new string[]{ "Ну как же хочется. Даже пить захотелось.", "Well, how do you want. I even wanted to drink." } }
        ,{ "str_everyday_event_no_10",             new string[]{ "Вместо лишних разговоров сделал приложил усилие в работе.", "Instead of unnecessary talk, he made an effort in work." } }
        ,{ "str_everyday_event_no_11",             new string[]{ "Теперь по ночам на меня оттуда кто то смотрит.", "Now at night someone is looking at me from there." } }
        ,{ "str_everyday_event_no_12",             new string[]{ "Сохранил силы для будущих свершений.", "Saved strength for future achievements." } }
        ,{ "str_everyday_event_no_13",             new string[]{ "Зануда! От скуки наделал новых ошибок.", "A bore! From boredom I made new mistakes." } }

        //фразы в лифте
        ,{ "str_elevator_event_0",              new string[]{ "Говорят, если прыгать в лифте, то он остановится. 2 часа прыгал. И правда застрял. Это весело.", "They say that if you jump in the elevator, then it will stop. 2 hours jumping. And the truth is stuck. It's fun." } }
        ,{ "str_elevator_event_1",              new string[]{ "Встретил девушку в лифте. Долго целовался, пока не понял, что это зеркало. Потерян 1 час.", "Met a girl in the elevator. Kissed for a long time, until he realized that it was a mirror. Lost 1 hour." } }
        ,{ "str_elevator_event_2",              new string[]{ "Прокатился с ветерком. Зарядился энергией.", "A ride with the breeze. Energized." } }
        ,{ "str_elevator_event_3",              new string[]{ "Укачало в лифте.", "Swayed in the elevator." } }
        ,{ "str_elevator_event_4",              new string[]{ "Прищемило ухо дверями лифта. И не спрашивайте как.", "Pinched the ear with elevator doors. And do not ask how." } }
        ,{ "str_elevator_event_5",              new string[]{ "Лифт засосало в черную дыру. Из-за физических законов вернулся в прошлое.", "The elevator sucked into the black hole. Due to physical laws, he returned to the past." } }
        ,{ "str_elevator_event_6",              new string[]{ "Встретил крысу. Решили обменяться хвостами, чтобы накормить друг друга. Добавлена сытость.", "Met a rat. We decided to exchange tails to feed each other. Satiety added." } }


        //GUI
        ,{ "str_december",              new string[]{ "декабря",  "December" } }
        ,{ "str_january",              new string[]{ "января",  "January" } }

        //mainmenu
        ,{ "mainLabelText",              new string[]{ "Новогодний офис", "Christmas office" } }
        ,{ "continueText",              new string[]{ "Продолжить",  "Continue" } }
        ,{ "newgameText",              new string[]{ "Новая игра",  "New Game" } }
        ,{ "optionsText",              new string[]{ "Инструкция",  "Instruction" } }
        ,{ "developersText",              new string[]{ "Создатели",  "Developers" } }
        ,{ "exitText",              new string[]{ "Выход",  "Exit" } }
        ,{ "developers",              new string[]{ "Титры",  "Developers" } }
        ,{ "instruction",              new string[]{ "    Ваш босс запер вас на новогодние праздники. Вы должны успеть доделать проект до 9:00 11 января. Прогресс проекта показывается полоской внизу экрана." + 
"\n    На своем 3 этаже вы можете перемещаться свободно." +
"Чтобы попасть на другие этажи, вы должны сделать электронный ключ на необходимый этаж и дождаться вечера. Именно тогда охрана начинает праздновать. Ночью вы можете собирать предметы на других этажах." +
 "Но в 9:00 вы обязаны вернуться на свой этаж, иначе охрана вас поймает." +
"\n    Вам следует иногда кушать, пить, спать, лечить травмы и болезни. В слесарном столе можно построить много нужных и не очень вещей." +
 "Здесь можно собрать топор, которым вы можете разрушать мусорные кучи. Они препятствуют передвижению по другим офисам. Если при действии иконка инвентаря в правом верхнем углу мигает, значит вам не хватает ресурсов." +
 "\n   Приятной игры!",
            "    Your boss has locked you on New Year's holidays. You must have time to complete the project before 9:00 on January 11. Project progress is indicated by a bar at the bottom of the screen. " +
 "\n   On your 3rd floor you can move around freely." +
 "To get to other floors, you must make an electronic key to the required floor and wait for the evening. It is then that the guards begin to celebrate. At night, you can collect items on other floors." +
"But at 9:00 you must return to your floor, otherwise the security will catch you." +
 "\n   You should sometimes eat, drink, sleep, treat injuries and illnesses. In a bench table you can build a lot of necessary and not so things." +
 "Here you can assemble an ax with which you can destroy garbage heaps. They impede movement to other offices. If during the action the inventory icon in the upper right corner flashes, then you do not have enough resources." +
 "\n   Have a nice game!" } }

        //HUD
        ,{ "str_winLabel",              new string[]{ "Поздравляем! Вы вовремя завершили проект. К сожалению, после ваших действий офис не подлежит восстановлению.", "Congratulations! You completed the project on time. Unfortunately, after your actions, the office cannot be restored." } }
        ,{ "str_winMenu",              new string[]{ "Меню",  "Menu" } }
        ,{ "str_yesDialog",              new string[]{ "Да",  "Yes" } }
        ,{ "str_noDialog",              new string[]{ "Нет",  "No" } }
        ,{ "str_itemsCraft",              new string[]{ "Инвентарь",  "Inventory" } }
        ,{ "str_labelMenu",              new string[]{ "Пауза",  "Pause" } }
        ,{ "str_optionsMenu",              new string[]{ "Настройки",  "Options" } }
        ,{ "str_menuMenu",              new string[]{ "Меню",  "Menu" } }
        ,{ "str_backMenu",              new string[]{ "Назад",  "Back" } }
        ,{ "str_labelLose",              new string[]{ "Ты проиграл...",  "You lose..." } }
        ,{ "str_menuLose",              new string[]{ "Меню",  "Menu" } }
        ,{ "str_soundLabel",              new string[]{ "Громкость звуков",  "Sound Volume" } }
        ,{ "str_muteSound",              new string[]{ "Отключить",  "Mute" } }
        ,{ "str_musicLabel",              new string[]{ "Громкость музыки",  "Music Volume" } }
        ,{ "str_muteMusic",              new string[]{ "Отключить",  "Mute" } }
        ,{ "str_chooseElevator",              new string[]{ "Выберите этаж \n У вас должен быть электронный ключ", "Choose elevator \n You must have an electronic key" } }
        ,{ "str_dealerLabel",              new string[]{ "Продавец",  "Dealer" } }
        ,{ "str_playerDealer",              new string[]{ "Ты",  "You" } }
        ,{ "str_newitemsLabel",              new string[]{ "Найденные предметы",  "Finded items" } }
        ,{ "str_playerItems",              new string[]{ "Инвентарь",  "Inventory" } }
        ,{ "str_playerInfo",              new string[]{ "Ты",  "You" } }
        ,{ "str_craftLabel",              new string[]{ "Создать предмет",  "Craft item" } }


        //окна информации
        ,{ "str_info",              new string[]{ "Ого! Тут коллега по несчастью. Может мне удастся обменяться с ним предметами?", "Wow! Here is a colleague in misfortune. Can I manage to exchange objects with him?" } }
        ,{ "str_info1",              new string[]{ "Неужели это древнейший вид работников нашей компании? Системный аадминистратор. Говорят, что он появился здесь задолго до строительства здания.", "Is it really the oldest kind of employees of our company? System Administrator. They say that he appeared here long before the construction of the building." } }
        ,{ "str_info2",              new string[]{ "Здесь открывается прекрасный вид на праздничный город. Интересно, кто эти люди большой рогаткой на крыше? И почему с ними свинья?", "It offers a beautiful view of the festive city. I wonder who these people are big slingshot on the roof? And why a pig with them?" } }
        ,{ "str_info3",              new string[]{ "Ого! Я вижу здесь много забытых и никому ненужных вещей. Пожалуй, заберу все себе.", "Wow! I see here a lot of forgotten and unnecessary things to nobody. Perhaps I’ll take everything for myself." } }
        ,{ "str_info4",              new string[]{ "Кричал о помощи и махал руками прохожим. Прохожие радостно кричали и махали руками мне в ответ.", "Shouted for help and waved hands to passers-by. Passers-by shouted joyfully and waved their hands at me in response." } }
        ,{ "str_info5",              new string[]{ "Тень человека с гитарой кажется такой величественной.", "The shadow of a man with a guitar seems so magnificent." } }
        ,{ "str_info6",              new string[]{ "Табличка. \n\"Кучу мусора перед входом не трогать! Это не мусор, а многомиллионный проект! \" \n Наверное я зря ее разломал.", "Nameplate. \n \"Do not touch the pile of rubbish in front of the entrance!This is not rubbish, but a multi - million dollar project! \" \n I guess I broke it in vain." } }
        ,{ "str_info7",              new string[]{ "Название компании \"Davay Tselovay\". Интересно, почему так назвали?", "Company name \"Davay Tselovay \". I wonder why they called it that?" } }
        ,{ "str_info8",              new string[]{ "Сам себе вылечил зубы. Из-за работы 10 лет не был у стоматолога.", "I have cured my teeth. Because of work, I have not visited a dentist for 10 years." } }
        ,{ "str_info9",              new string[]{ "Это же парень из нашей компании. Он пропал после корпоративного праздника. Не стоит злоупотреблять вишневым соком!", "This is the guy from our company. He disappeared after a corporate holiday. Do not abuse cherry juice!" } }


        ,{ "str_late",              new string[]{ "Сработала сигнализация. Охрана задержала вас. Вы потеряли несколько часов.", "The alarm went off. The guard has delayed you. You have lost a few hours." } }

        ,{ "str_time_lose",    new string[]{ "Проект не был завершен вовремя. Вы уволены. Но, благодаря вашей ловкости, вы успели взять с собой пару жареных крыс.", "The project was not completed on time. You're fired. But, thanks to your dexterity, you managed to take a couple of fried rats with you." } }
        ,{ "str_health_lose",              new string[]{ "С занозой в пальце вы были госпитализированы.", "With a splinter in your finger, you were hospitalized." } }
        ,{ "str_energy_lose",              new string[]{ "Вы проспали все праздники и проснулись на коленях у начальника. И да, вы уволены.", "You overslept all the holidays and woke up on your boss’s lap. And yes, you are fired." } }
        ,{ "str_food_lose",              new string[]{ "Желудок не выдержал и ушел искать пищу сам. А без желудка работать запрещено.", "The stomach could not stand it and went away to look for food itself. And it’s forbidden to work without a stomach." } }
        ,{ "str_water_lose",              new string[]{ "Без воды вы усохли настолько, что смогли пролезть под дверью и оказаться на свободе. Но без работы.", "Without water, you have dried out so much that you could crawl under the door and be free. But without work." } }

    };


}
