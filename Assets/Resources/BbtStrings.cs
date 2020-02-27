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
         { "str_inv_cure",                      new string[]{ "Лекарство",              "Cure" } }
        ,{ "str_inv_bandage",                   new string[]{ "Бинт",                   "Bandage" } }
        ,{ "str_inv_antibiotic",                new string[]{ "Антибиотик",             "Antibiotic" } }
        ,{ "str_inv_friedmushrooms",            new string[]{ "Жареные грибы",          "Soupe" } }
        ,{ "str_inv_energybar",                 new string[]{ "Батончик",               "Energybar" } }
        ,{ "str_inv_noodles",                   new string[]{ "Доширак",                "Noodles" } }
        ,{ "str_inv_rat",                       new string[]{ "Крыса",                  "Rat" } }
        ,{ "str_inv_mushrooms",                 new string[]{ "Грибы",                  "Mushrooms" } }
        ,{ "str_inv_water",                     new string[]{ "Вода",                   "Water" } }
        ,{ "str_inv_coffee",                    new string[]{ "Кофе",                   "Coffee" } }
        ,{ "str_inv_juice",                     new string[]{ "Сок",                    "Juice" } }
        ,{ "str_inv_cola",                      new string[]{ "Кола",                   "Cola" } }
        ,{ "str_inv_axe",                       new string[]{ "Топор",                  "Axe" } }
        ,{ "str_inv_stuff",                     new string[]{ "Компоненты",             "Stuff" } }
        ,{ "str_inv_money",                     new string[]{ "Деньги",                 "Money" } }
        ,{ "str_inv_humus",                     new string[]{ "Перегной",               "Humus" } }
        ,{ "str_inv_friedrat",                  new string[]{ "Жареная крыса",          "Fried rat" } }
        ,{ "str_inv_computer",                  new string[]{ "Компьютер",              "Fried rat" } }
        ,{ "str_inv_crafttable",                new string[]{ "Крафтовый стол",         "Fried rat" } }
        ,{ "str_inv_wires",                     new string[]{ "Провода",                "Fried rat" } }
        ,{ "str_inv_fir",                       new string[]{ "Елочка",                 "Fried rat" } }
        ,{ "str_inv_key1",                      new string[]{ "Ключ 1 этаж",            "Fried rat" } }
        ,{ "str_inv_key2",                      new string[]{ "Ключ 2 этаж",            "Fried rat" } }
        ,{ "str_inv_condey",                    new string[]{ "Кондиционер",            "Fried rat" } }
        ,{ "str_inv_corob",                     new string[]{ "Коробки",                "Fried rat" } }
        ,{ "str_inv_key4",                      new string[]{ "Ключ 4 этаж",            "Fried rat" } }
        ,{ "str_inv_key5",                      new string[]{ "Ключ 5 этаж",            "Fried rat" } }
        ,{ "str_inv_kreslo",                    new string[]{ "Кресло",                 "Fried rat" } }
        ,{ "str_inv_sofa",                      new string[]{ "Диван",                  "Fried rat" } }
        ,{ "str_inv_monitor",                   new string[]{ "Монитор",                "Fried rat" } }
        ,{ "str_inv_mushtrap",                  new string[]{ "Грибной сад",            "Fried rat" } }
        ,{ "str_inv_rattrap",                   new string[]{ "Крысиная ловушка",       "Fried rat" } }
        ,{ "str_inv_watertrap",                 new string[]{ "Водяной фильтр",         "Fried rat" } }
        ,{ "str_inv_wood",                      new string[]{ "Дерево",                 "Fried rat" } }
        ,{ "str_inv_zapchasti",                 new string[]{ "Запчасти",               "Fried rat" } }

        //параметры
        ,{ "str_param_0",                       new string[]{ "Жажда",                  "Humus" } }
        ,{ "str_param_1",                       new string[]{ "Сытость",                "Humus" } }
        ,{ "str_param_2",                       new string[]{ "Энергия",                "Humus" } }
        ,{ "str_param_3",                       new string[]{ "Здоровье",               "Humus" } }
        ,{ "str_desease_0",                     new string[]{ "Рана",                   "Humus" } }
        ,{ "str_desease_1",                     new string[]{ "Болезнь",                "Humus" } }
        ,{ "str_project",                       new string[]{ "Проект",                 "Humus" } }

        //inventory description
        ,{ "str_inv_cure_desc",                 new string[]{ "Средство от легкой болезни",                                             "Cure" } }
        ,{ "str_inv_bandage_desc",              new string[]{ "Средство от физических ранений",                                         "Bandage" } }
        ,{ "str_inv_antibiotic_desc",           new string[]{ "Средство от тяжелой формы болезни",                                      "Antibiotic" } }
        ,{ "str_inv_friedmushrooms_desc",       new string[]{ "Вкусные жареные грибочки",                                               "Soupe" } }
        ,{ "str_inv_energybar_desc",            new string[]{ "Энергетический батончик не только восполняет сытость, но и энергию",     "Energybar" } }
        ,{ "str_inv_noodles_desc",              new string[]{ "Питательная лапша быстрого приготовления",                               "Noodles" } }
        ,{ "str_inv_rat_desc",                  new string[]{ "Не считая дивана, единственное мясо в этом офисе",                       "Rat" } }
        ,{ "str_inv_mushrooms_desc",            new string[]{ "Нету сушей, нету пиццы, ешьте вы пучки грибицы",                         "Mushrooms" } }
        ,{ "str_inv_water_desc",                new string[]{ "Чистейшая вода",                                                         "Water" } }
        ,{ "str_inv_coffee_desc",               new string[]{ "Топливо для программиста",                                               "Coffee" } }
        ,{ "str_inv_juice_desc",                new string[]{ "Кто додумался давить сок из чеснока?",                                   "Juice" } }
        ,{ "str_inv_cola_desc",                 new string[]{ "Не употреблять вместе с кофе!",                                          "Cola" } }
        ,{ "str_inv_axe_desc",                  new string[]{ "Убойный инструмент против босса",                                        "Axe" } }
        ,{ "str_inv_stuff_desc",                new string[]{ "Из гвоздя, скрепки и бумажки сделаю спутниковое ТВ",                     "Stuff" } }
        ,{ "str_inv_money_desc",                new string[]{ "Не в них сила",                                                          "Money" } }
        ,{ "str_inv_humus_desc",                new string[]{ "Сила в перегное!",                                                       "Humus" } }
        ,{ "str_inv_friedrat_desc",             new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_computer_desc",             new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_crafttable_desc",           new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_wires_desc",                new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_fir_desc",                  new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_key1_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_key2_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_condey_desc",               new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_corob_desc",                new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_key4_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_key5_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_kreslo_desc",               new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_sofa_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_monitor_desc",              new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_mushtrap_desc",             new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_rattrap_desc",              new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_watertrap_desc",            new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_wood_desc",                 new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }
        ,{ "str_inv_zapchasti_desc",            new string[]{ "С хрустящим хвостиком!",                                                 "Fried rat" } }


        //фразы торговца
        ,{ "str_start_trade",                   new string[]{ "Что предложите?",              "What the fuck?" } }
        ,{ "str_not_good_trade",                new string[]{ "Так не пойдет. Давай больше.", "Fuck you" } }
        ,{ "str_good_trade_1",                  new string[]{ "Думаю это справедливо.",       "fuck you and fuck me" } }
        ,{ "str_good_trade_2",                  new string[]{ "Отличное предложение!",        "looser" } }
        ,{ "str_good_trade_3",                  new string[]{ "Какая щедрость!",              "I'm your slave!" } }

        //фразы состояния
        ,{ "str_status_player_0",               new string[]{ "Моя лень меня бережет!",  "I'm your slave!" } }
        ,{ "str_status_player_1",               new string[]{ "От голода скоро начну грызть стулья",  "I'm your slave!" } }
        ,{ "str_status_player_2",               new string[]{ "Ради еды готов прогрызть выход наружу.",  "I'm your slave!" } }
        ,{ "str_status_player_3",               new string[]{ "Только что во рту почувствовал движение перекати-поля.",  "I'm your slave!" } }
        ,{ "str_status_player_4",               new string[]{ "Может создать замкнутую цепь круговорота жидкости в организме?",  "I'm your slave!" } }
        ,{ "str_status_player_5",               new string[]{ "Обыскал весь офис. Не нашел ни одного подорожника.",  "I'm your slave!" } }
        ,{ "str_status_player_6",               new string[]{ "Против любой болезни поможет клизма!",  "I'm your slave!" } }
        ,{ "str_status_player_7",               new string[]{ "Я доделал проект! О нет. Мне это приснилось.",  "I'm your slave!" } }
        ,{ "str_status_player_8",               new string[]{ "Подушка - мой лучший друг.",  "I'm your slave!" } }

        //фразы рандомных событий
        ,{ "str_random_event_0",                new string[]{ "Уснул на клавиатуре. Удалил половину проекта. Удалено 3% проекта",  "slave" } }
        ,{ "str_random_event_1",                new string[]{ "Улыбнулся пробегающей крысе. Та упала навзничь. Получена крыса.",  "slave" } }
        ,{ "str_random_event_2",                new string[]{ "Над головой загорелась лампочка. Упала и разбилась. Проект продвинулся, но стекла остались на лице. Добавлено 2% проекта. Получена травма",  "slave" } }
        ,{ "str_random_event_3",                new string[]{ "Ночью дедушка Мороз батончик и колу мне принес. Оливье бы. Получены батончик и кола.",  "slave" } }
        ,{ "str_random_event_4",                new string[]{ "Для ускорения работы вы попытались дополнительно работать ногами на клавиаутуре. Она не выдержала запах ваших ног. Пришлось потратить не мало времени, чтобы уговорить ее работать. Потрачена энергия.",  "slave" } }
        ,{ "str_random_event_5",                new string[]{ "Не стоило жевать найденную шоколадку за компом в пыльном помещении. Мой чих был слышен на другом конце города. Я похож на пульверизатор. Потрачена энергия на чистку монитора.",  "slave" } }
        ,{ "str_random_event_6",                new string[]{ "-Хммм, а что это ты жуешь?\n-Не знаю! Само приползло. \n Добавлена сытость.",  "slave" } }
        ,{ "str_random_event_7",                new string[]{ "Нашел в вентиляции застрявшего коллегу, который пропал неделю назад. Его тоже оставляли закончить проект, но во время побега он застрял. Забрал его собранные припасы. Сейчас они мне нужнее. Добавлены батончик и сок.",  "slave" } }
        ,{ "str_random_event_8",                new string[]{ "Мне снилось как крысы спускались на веревках, как шпионы, чтобы выкрасть кусочек еды. Я думал это сон, пока не обнаружил, что пропала пачка лапши.",  "slave" } }
        ,{ "str_random_event_9",                new string[]{ "Каждый веселится по разному в Новый Год. Кто то танцует. Кто то пускает салюты. А кто то запускает горящего поросенка в здания. Пришлось потратить немного воды, чтобы потушить бедолагу.",  "slave" } }
        ,{ "str_random_event_10",               new string[]{ "Решил поспать на один глаз. Выспался наполовину.",  "slave" } }
        ,{ "str_random_event_11",               new string[]{ "Протек унитаз. Зато получил немного воды.",  "slave" } }
        ,{ "str_random_event_12",               new string[]{ "Нашел старую обучвь коллеги. Набрал немного грибов.",  "slave" } }
        ,{ "str_random_event_13",               new string[]{ "Нашел кошелек босса. Назначил себе премию.",  "slave" } }

        //фразы ежедневного выбора
        ,{ "str_everyday_event_0",              new string[]{ "В окно на большой скорости влетел разовый поросенок. Оставил на черный день. Покормить?",  "slave" } }
        ,{ "str_everyday_event_1",              new string[]{ "Решил поспать на клавиатуре. На ней мягче. Стоит ли?",  "slave" } }
        ,{ "str_everyday_event_2",              new string[]{ "Нашел лапшу в стене. Побробовать съесть?",  "slave" } }
        ,{ "str_everyday_event_3",              new string[]{ "Открыл дорогие сигары босса. Стоит ли закурить?",  "slave" } }
        ,{ "str_everyday_event_4",              new string[]{ "Есть возможность подсмотреть проект у коллеги. Сделать это?",  "slave" } }
        ,{ "str_everyday_event_5",              new string[]{ "Чувствую беспомощность. Крикнуть ли на весь офис просьбу о помощи?",  "slave" } }
        ,{ "str_everyday_event_6",              new string[]{ "Тараканы выстроились напротив меня и приготовились к атаке. Готовиться к обороне?",  "slave" } }
        ,{ "str_everyday_event_7",              new string[]{ "Нашел неизвестный журнал с надписью ХХХ. Почитать?",  "slave" } }
        ,{ "str_everyday_event_8",              new string[]{ "Обнаружил сейф коллеги. Там может быть все что угодно. Открыть?",  "slave" } }
        ,{ "str_everyday_event_9",              new string[]{ "За окном что то блестит. Лизнуть это?",  "slave" } }
        ,{ "str_everyday_event_10",             new string[]{ "Ко мне пришел коллега для обмена. Предлагает лапшу. Хочет лекарство",  "slave" } }
        ,{ "str_everyday_event_11",             new string[]{ "Нашел дыру в стене. Посмотреть?",  "slave" } }
        ,{ "str_everyday_event_12",             new string[]{ "Решил слепить снеговика. Стоит ли?",  "slave" } }
        ,{ "str_everyday_event_13",             new string[]{ "Громко играл на электрогитаре. Разбить ее о стену?",  "slave" } }

        //ответа да ежедневного выбора
        ,{ "str_everyday_event_yes_0",              new string[]{ "Снял с него какие-то тряпки. Пригодятся в качестве бинтов.",  "slave" } }
        ,{ "str_everyday_event_yes_1",              new string[]{ "Во сне случайно напечатал часть проекта.",  "slave" } }
        ,{ "str_everyday_event_yes_2",              new string[]{ "Попробовал укусить. Оказалось это электрическая проводка.",  "slave" } }
        ,{ "str_everyday_event_yes_3",              new string[]{ "Сработала система пожаротушения. Набрал немного воды.",  "slave" } }
        ,{ "str_everyday_event_yes_4",              new string[]{ "Кто мог подумать, что его проект отличается от моего? Наделал много ошибок в своем.",  "slave" } }
        ,{ "str_everyday_event_yes_5",              new string[]{ "Тараканы из всех щелей принесли мне угощение. Теперь не смогу уснуть.",  "slave" } }
        ,{ "str_everyday_event_yes_6",              new string[]{ "Огонь в глазах, храбрость в сердце. И я побежден в битве 'один на один'.",  "slave" } }
        ,{ "str_everyday_event_yes_7",              new string[]{ "Теперь долго не смогу уснуть.",  "slave" } }
        ,{ "str_everyday_event_yes_8",              new string[]{ "Ура! Склад!",  "slave" } }
        ,{ "str_everyday_event_yes_9",              new string[]{ "Ну почему я везде сую свой язык? Теперь он примерз.",  "slave" } }
        ,{ "str_everyday_event_yes_10",             new string[]{ "Поговорили. Обменялись. Все довольны.",  "slave" } }
        ,{ "str_everyday_event_yes_11",             new string[]{ "Ура! Заначка!",  "slave" } }
        ,{ "str_everyday_event_yes_12",             new string[]{ "Потратил немало сил. Зато работать стало веселее.",  "slave" } }
        ,{ "str_everyday_event_yes_13",             new string[]{ "Придется купить начальнику новую гитару.",  "slave" } }

        //ответа нет ежедневного выбора
        ,{ "str_everyday_event_no_0",              new string[]{ "Потратил силы чтобы отогнать наглого поросенка от моей еды.",  "slave" } }
        ,{ "str_everyday_event_no_1",              new string[]{ "Как же хочется спать.",  "slave" } }
        ,{ "str_everyday_event_no_2",              new string[]{ "От обиды съел свою лапшу.",  "slave" } }
        ,{ "str_everyday_event_no_3",              new string[]{ "Правильно! Лучше буду больше кушать.",  "slave" } }
        ,{ "str_everyday_event_no_4",              new string[]{ "Хе-хе. Сам написал.",  "slave" } }
        ,{ "str_everyday_event_no_5",              new string[]{ "Нет. Я умный человек. Лучше сохраню энергию.",  "slave" } }
        ,{ "str_everyday_event_no_6",              new string[]{ "Рейд тараконов прошел успешно.",  "slave" } }
        ,{ "str_everyday_event_no_7",              new string[]{ "Неудовлетворенное любопытство вызвало жажду.",  "slave" } }
        ,{ "str_everyday_event_no_8",              new string[]{ "Сказал об этом коллеге. Получил помощь в проекте.",  "slave" } }
        ,{ "str_everyday_event_no_9",              new string[]{ "Ну как же хочется. Даже пить захотелось.",  "slave" } }
        ,{ "str_everyday_event_no_10",             new string[]{ "Вместо лишних разговоров сделал приложил усилие в работе.",  "slave" } }
        ,{ "str_everyday_event_no_11",             new string[]{ "Теперь по ночам на меня оттуда кто то смотрит.",  "slave" } }
        ,{ "str_everyday_event_no_12",             new string[]{ "Сохранил силы для будущих свершений.",  "slave" } }
        ,{ "str_everyday_event_no_13",             new string[]{ "Зануда! От скуки наделал новых ошибок.",  "slave" } }

        //фразы в лифте
        ,{ "str_elevator_event_0",              new string[]{ "Говорят, если прыгать в лифте, то он остановится. 2 часа прыгал. И правда застрял. Это весело. Потеряно 2 часа.",  "slave" } }
        ,{ "str_elevator_event_1",              new string[]{ "Встретил девушку в лифте. Долго целовался, пока не понял, что это зеркало. Потерян 1 час.",  "slave" } }
        ,{ "str_elevator_event_2",              new string[]{ "Прокатился с ветерком. Зарядился энергией.",  "slave" } }
        ,{ "str_elevator_event_3",              new string[]{ "Укачало в лифте.",  "slave" } }
        ,{ "str_elevator_event_4",              new string[]{ "Прищемило ухо дверями лифта. И не спрашивайте как.",  "slave" } }
        ,{ "str_elevator_event_5",              new string[]{ "Лифт засосало в черную дыру. Из-за физических законов вернулся в прошлое.",  "slave" } }
        ,{ "str_elevator_event_6",              new string[]{ "Встретил крысу. Решили обменяться хвостами, чтобы накормить друг друга. Добавлена сытость.",  "slave" } }


        //GUI
        ,{ "str_december",              new string[]{ "декабря",  "December" } }
        ,{ "str_january",              new string[]{ "января",  "January" } }

        //mainmenu
        ,{ "mainLabelText",              new string[]{ "Адский офис",  "Hellish Office" } }
        ,{ "continueText",              new string[]{ "Продолжить",  "Continue" } }
        ,{ "newgameText",              new string[]{ "Новая игра",  "New Game" } }
        ,{ "optionsText",              new string[]{ "Инструкция",  "Instruction" } }
        ,{ "developersText",              new string[]{ "Создатели",  "Developers" } }
        ,{ "exitText",              new string[]{ "Выход",  "Exit" } }
        ,{ "developers",              new string[]{ "Титры",  "titri" } }
        ,{ "instruction",              new string[]{ "Текст инструкции",  "Instruction Text" } }

        //HUD
        ,{ "str_winLabel",              new string[]{ "Победа!",  "Congratulations" } }
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
        ,{ "str_chooseElevator",              new string[]{ "Выберите этаж \n У вас должен быть электронный ключ",  "Choose elevator" } }
        ,{ "str_dealerLabel",              new string[]{ "Продавец",  "Dealer" } }
        ,{ "str_playerDealer",              new string[]{ "Ты",  "You" } }
        ,{ "str_newitemsLabel",              new string[]{ "Найденные предметы",  "Finded items" } }
        ,{ "str_playerItems",              new string[]{ "Инвентарь",  "Inventory" } }
        ,{ "str_playerInfo",              new string[]{ "Ты",  "You" } }
        ,{ "str_craftLabel",              new string[]{ "Создать предмет",  "Craft item" } }


        //окна информации
        ,{ "str_info",              new string[]{ "инфо",  "info" } }
        ,{ "str_info1",              new string[]{ "инфо1",  "info1" } }
        ,{ "str_info2",              new string[]{ "инфо2",  "info2" } }
        ,{ "str_info3",              new string[]{ "инфо3",  "info3" } }
        ,{ "str_info4",              new string[]{ "инфо4",  "info4" } }
        ,{ "str_info5",              new string[]{ "инфо5",  "info5" } }
        ,{ "str_info6",              new string[]{ "инфо6",  "info6" } }
        ,{ "str_info7",              new string[]{ "инфо7",  "info7" } }
        ,{ "str_info8",              new string[]{ "инфо8",  "info8" } }
        ,{ "str_info9",              new string[]{ "инфо9",  "info9" } }

        ,{ "str_late",              new string[]{ "Сработала сигнализация. Охрана задержала вас. Вы потеряли несколько часов.",  "late" } }

    };


}
