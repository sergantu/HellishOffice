﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BbtStrings
{

    public static string GetStr( string bbt )
    {
        if ( STR.ContainsKey(bbt) )
        {
            return STR[bbt][0];
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
        ,{ "str_inv_soupe",                     new string[]{ "Суп",                    "Soupe" } }
        ,{ "str_inv_energybar",                 new string[]{ "Батончик",               "Energybar" } }
        ,{ "str_inv_noodles",                   new string[]{ "Доширак",                "Noodles" } }
        ,{ "str_inv_rat",                       new string[]{ "Крыса",                  "Rat" } }
        ,{ "str_inv_mushrooms",                 new string[]{ "Грибы",                  "Mushrooms" } }
        ,{ "str_inv_water",                     new string[]{ "Вода",                   "Water" } }
        ,{ "str_inv_coffee",                    new string[]{ "Кофе",                   "Coffee" } }
        ,{ "str_inv_juice",                     new string[]{ "Сок",                    "Juice" } }
        ,{ "str_inv_cola",                      new string[]{ "Кола",                   "Cola" } }
        ,{ "str_inv_axe",                       new string[]{ "Топор",                  "Axe" } }
        ,{ "str_inv_stuff",                     new string[]{ "Хлам",                   "Stuff" } }
        ,{ "str_inv_money",                     new string[]{ "Деньги",                 "Money" } }
        ,{ "str_inv_humus",                     new string[]{ "Перегной",               "Humus" } }
        ,{ "str_inv_friedrat",                  new string[]{ "Жареная крыса",          "Fried rat" } }

        //параметры
        ,{ "str_param_0",                       new string[]{ "Жажда",               "Humus" } }
        ,{ "str_param_1",                       new string[]{ "Сытость",               "Humus" } }
        ,{ "str_param_2",                       new string[]{ "Энергия",               "Humus" } }
        ,{ "str_param_3",                       new string[]{ "Здоровье",               "Humus" } }
        ,{ "str_desease_0",                       new string[]{ "Рана",               "Humus" } }
        ,{ "str_project",                       new string[]{ "Проект",               "Humus" } }

        //inventory description
        ,{ "str_inv_cure_desc",                 new string[]{ "Средство от легкой болезни",                                             "Cure" } }
        ,{ "str_inv_bandage_desc",              new string[]{ "Средство от физических ранений",                                         "Bandage" } }
        ,{ "str_inv_antibiotic_desc",           new string[]{ "Средство от тяжелой формы болезни",                                      "Antibiotic" } }
        ,{ "str_inv_soupe_desc",                new string[]{ "Вкусный суп из грибочков",                                               "Soupe" } }
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

        //фразы в лифте
        ,{ "str_elevator_event_0",              new string[]{ "Говорят, если прыгать в лифте, то он остановится. 2 часа прыгал. И правда застрял. Это весело. Потеряно 2 часа.",  "slave" } }
        ,{ "str_elevator_event_1",              new string[]{ "Встретил девушку в лифте. Долго целовался, пока не понял, что это зеркало. Потерян 1 час.",  "slave" } }
        ,{ "str_elevator_event_2",              new string[]{ "Прокатился с ветерком. Зарядился энергией.",  "slave" } }
        ,{ "str_elevator_event_3",              new string[]{ "Укачало в лифте.",  "slave" } }
        ,{ "str_elevator_event_4",              new string[]{ "Прищемило ухо дверями лифта. И не спрашивайте как.",  "slave" } }
        ,{ "str_elevator_event_5",              new string[]{ "Лифт засосало в черную дыру. Из-за физических законов вернулся в прошлое.",  "slave" } }
        ,{ "str_elevator_event_6",              new string[]{ "Встретил крысу. Решили обменяться хвостами, чтобы накормить друг друга. Добавлена сытость.",  "slave" } }


    };


}
