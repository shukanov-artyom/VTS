using System.Collections.Generic;

namespace Agent.Helpers
{
    public static class PsaGlobalDictionary
    {
        private static IDictionary<string, string> dictionary = 
            new Dictionary<string, string>();

        static PsaGlobalDictionary()
        {
            /*dictionary.Add(@"@P6604-CITACT@\*1","Основные параметры 1");
            dictionary.Add(@"@P6604-CITACT@\*2", "Основные параметры 2");
            dictionary.Add(@"@P5424-CITACT","Обороты двигателя");
            dictionary.Add(@"@P1530-CITACT@\*","об/мин");
            dictionary.Add(@"@P36087-CITACT","Синхронизация кулачкового и коленчатого валов");
            dictionary.Add(@"@P1992-citact","калькулятор СМ");
            dictionary.Add(@"@P8446-citact","Топливные присадки");
            dictionary.Add(@"@P3935-citact","Многофункциональный индикатор");
            dictionary.Add(@"@P13160-citact","Радиоприемник или радиотелефон (часть радиоприемника)");
            dictionary.Add(@"@P3668-citact","CD-проигрыватель");
            dictionary.Add(@"@P5854-citact","панель управления");
            dictionary.Add(@"@P13638-citact","Подушки безопасности водителя и пассажира *");
            dictionary.Add(@"@P9261-citact","Электронный модуль индикаторов и счетчик пробега");
            dictionary.Add(@"@P6615-citact","Климатическая установка");
            dictionary.Add(@"@P6615-CITACT", "Климатическая установка");
            dictionary.Add(@"@P5682-citact","Верхняя часть колонки");
            dictionary.Add(@"@P19223-citact","Электронный модуль двери водителя");
            dictionary.Add(@"@P19224-citact","Модуль двери пассажира");
            dictionary.Add(@"@P8078-citact","Впрыск");
            dictionary.Add(@"@P428-citact","ABS *");
            dictionary.Add(@"@P3165-citact","Дополнительный обогрев");
            dictionary.Add(@"@P13306-citact","Корректор Правой Фары");
            dictionary.Add(@"@P13307-citact","Корректор Левой Фары");
            dictionary.Add(@"@P11833-THESAU","Ни одна ошибка не внесена в память");
            dictionary.Add(@"@P30709-CITACT","prйsence de codes dйfauts");
            dictionary.Add(@"@P3468-CITACT","неизвестен");
            dictionary.Add(@"@P6604-CITACT","Основные параметры");
            dictionary.Add(@"@P8120-CITACT","Измерянное давление в топливной системе");
            dictionary.Add(@"@P2954-CITACT","бар");
            dictionary.Add(@"@P36090-CITACT","Разница между замерянным Давлением топлива и номинальным давлением топлива");
            dictionary.Add(@"@P35639-CITACT","Циклическое соотношение открытия регулятора давления топлива");
            dictionary.Add(@"@T%","%");
            dictionary.Add(@"@P7014-CITACT","Затребованный объем впрыска");
            dictionary.Add(@"@P5187-CITACT","Коррекция расхода инжектора");
            dictionary.Add(@"@P5187-CITACT@\*1","Коррекция расхода инжектора 1");
            dictionary.Add(@"@P5187-CITACT@\*2","Коррекция расхода инжектора 2");
            dictionary.Add(@"@P5187-CITACT@\*3","Коррекция расхода инжектора 3");
            dictionary.Add(@"@P5187-CITACT@\*4","Коррекция расхода инжектора 4");
            dictionary.Add(@"@P10260-CITACT","Напряжение инжектор");
            dictionary.Add(@"@P10260-CITACT@\*1@\*4","Напряжение инжектор 1 и 4");
            dictionary.Add(@"@P10260-CITACT@\*2@\*3","Напряжение инжектор 2 и 3");
            dictionary.Add(@"@P2953-CITACT","Вольт");
            dictionary.Add(@"@P2755-CITACT@\*","Напряжение инжекторов");
            dictionary.Add(@"@P2424-CITACT","мг/ср");
            dictionary.Add(@"@P36091-CITACT","Замерянный объем воздуха");
            dictionary.Add(@"@P1518-CITACT","%");
            dictionary.Add(@"@P35640-CITACT","Циклическое соотношение открытия электроклапана системы рециркуляции выхлопных газов");
            dictionary.Add(@"@P35642-CITACT","Сила тока регулятора давления топлива");
            dictionary.Add(@"@TmA","mA");
            dictionary.Add(@"@P2763-CITACT","Опережение предварительного впрыска");
            dictionary.Add(@"@T°","°");
            dictionary.Add(@"@P2754-CITACT","Основное опережение системы впрыска");
            dictionary.Add(@"@P10261-CITACT@\+@P24379-CITACT","Отключение 3-его поршня (В случае наличия)	");
            dictionary.Add(@"@P10261-CITACT","Отключение 3-его поршня");
            dictionary.Add(@"@P24379-CITACT","(В случае наличия)");
            dictionary.Add(@"@P10533-CITACT","Давление турбокомпрессора");
            dictionary.Add(@"@P1517-CITACT@\*","миллибар");
            dictionary.Add(@"@P10279-CITACT","Номинальное давление в турбокомпрессоре");
            dictionary.Add(@"@P35643-CITACT","Циклическое соотношение открытия электроклапана Давления турбины");
            dictionary.Add(@"@P26901-CITACT","Атмосферное давление");
            dictionary.Add(@"@P11831-CITACT","Mb");
            dictionary.Add(@"@P6066-CITACT","Температура воды");
            dictionary.Add(@"@P408-CITACT", "°C");
            dictionary.Add(@"@@P6679-CITACT", "°C");
            dictionary.Add(@"@P36092-CITACT","Температура воздуха");
            dictionary.Add(@"@P5199-CITACT","Температура топлива");
            dictionary.Add(@"@T°C","°C");
            dictionary.Add(@"@P35641-CITACT","Циклическое соотношение открытия заслонки системы рециркуляции выхлопных газов");
            dictionary.Add(@"@P3226-CITACT","Информация об электросистеме");
            dictionary.Add(@"@P3126-CITACT","напряжение аккумулятора");
            dictionary.Add(@"@P36093-CITACT@\*1","Напряжение в цепи питания Датчиков 1 (кулачковые валы)");
            dictionary.Add(@"@P36094-CITACT@\*2","Напряжение в цепи питания Датчиков 2 (другие)");
            dictionary.Add(@"@TV","V");
            dictionary.Add(@"@P2652-CITACT","Реле предварительного пост - обогрева");
            dictionary.Add(@"@P10262-CITACT","Запрос на отключение системы кондиционирования");
            dictionary.Add(@"@P35644-CITACT","Реле низкой скорости группы мотовентилятора");
            dictionary.Add(@"@P36095-CITACT@\*","Скорость группы мотовентилятора");
            dictionary.Add(@"@P33397-CITACT", "Впрыск Топлива и Система очистки");
            dictionary.Add(@"@P1543-CITACT@\*", "Время впрыска");
            dictionary.Add(@"@P2416-CITACT", "мс");
            dictionary.Add(@"@P4896-CITACT", "Выключение системы впрыска при снижении ускорения");
            dictionary.Add(@"@P2411-CITACT@\*", "мВ");
            dictionary.Add(@"@P12030-CITACT", "Напряжение верхнего кислородного зонда");
            dictionary.Add(@"@P12031-CITACT", "Напряжение нижнего кислородного зонда");
            dictionary.Add(@"@P2286-CITACT", "Состояние регулированя обогащения смеси");
            dictionary.Add(@"@P2953-CITACT@\*", "Вольт");
            dictionary.Add(@"@P76948-THESAU@\*", "Вольт");
            dictionary.Add(@"@P12120-CITACT@\*4", "Время зарядки катушки цилиндра 4");
            dictionary.Add(@"@P12120-CITACT@\*3", "Время зарядки катушки цилиндра 3");
            dictionary.Add(@"@P12120-CITACT@\*2", "Время зарядки катушки цилиндра 2");
            dictionary.Add(@"@P12120-CITACT@\*1", "Время зарядки катушки цилиндра 1");

            dictionary.Add(@"@P6161-CITACT", "Данные по сажевому фильтру");
            dictionary.Add(@"@P27002-CITACT", "Статус сажевого фильтра");
            dictionary.Add(@"@P10534-CITACT", "Запрос на поддержку регенерации");
            dictionary.Add(@"@P10284-CITACT", "Верхняя температура конвертера катализатора");
            dictionary.Add(@"@P10285-CITACT", "Нижняя температура конвертера катализатора");
            dictionary.Add(@"@P27676-CITACT", "Разность входного и выходного давления фильтра");
            dictionary.Add(@"@P2956-CITACT", "Температура охлаждающей жидкости");
            dictionary.Add(@"@P10287-CITACT", "Объём потребляемого воздуха");
            dictionary.Add(@"@P13200-CITACT", "м3/ч");
            dictionary.Add(@"@P35646-CITACT", "Соотношение открытия заслонки подогревателя впускаемого воздуха");
            dictionary.Add(@"@P10289-CITACT", "Расход топлива, подаваемого во время фазы последующего впрыска");
            dictionary.Add(@"@P1534-CITACT", "мм3/ход поршня");
            dictionary.Add(@"@P10290-CITACT", "Фазировка после впрыска");
            dictionary.Add(@"@P35647-CITACT", "Суммарное количество топливной присадки");
            dictionary.Add(@"@Tg", "г");
            dictionary.Add(@"@P10536-CITACT", "Пробег со времени прошлой регенерации");
            dictionary.Add(@"@P2957-CITACT", "км");
            dictionary.Add(@"@P10292-CITACT@\*5", "Средний интервал пробега между пятью последними регенерациями");
            dictionary.Add(@"@P12392-CITACT", "Активация электропотребителей");

            dictionary.Add(@"@P5805-CITACT", "Информация о вождении");
            dictionary.Add(@"@P5423-CITACT", "Скорость автомобиля");
            dictionary.Add(@"@P1520-CITACT", "км/ч");
            dictionary.Add(@"@P13738-CITACT", "Положение педали газа");
            dictionary.Add(@"@P36097-CITACT", "Включенная передача");
            dictionary.Add(@"@P36098-CITACT", "Автоматическое определение передачи");
            dictionary.Add(@"@P21727-CITACT", "Сигнал с основной тормозной системы");
            dictionary.Add(@"@P21728-CITACT", "Сигнал с дополнительной тормозной системы");
            dictionary.Add(@"@P21729-CITACT", "Положение педали сцепления");
            dictionary.Add(@"@P21730-CITACT@\*2", "Положение педали газа точка 2");
            dictionary.Add(@"@P21730-CITACT@\*1", "Положение педали газа точка 1");
            dictionary.Add(@"@P10575-CITACT", "Внутреннее состояние ЖК-экрана одометра");
            dictionary.Add(@"@P5261-CITACT", "Кнопка яркости");
            dictionary.Add(@"@P3658-CITACT", "Параметры панели управления");
            dictionary.Add(@"@P3675-CITACT", "Единицы измерения счётика пройденного пути");
            dictionary.Add(@"@P18279-CITACT", "Электропитание основных блоков");
            dictionary.Add(@"@P8875-CITACT", "Питание на выключателях задних электростеклоподъёмников");
            dictionary.Add(@"@P6606-CITACT", "Питание датчика дождя");
            dictionary.Add(@"@P17647-CITACT", "Контроль за работающим двигателем");
            dictionary.Add(@"@P8877-CITACT", "Деактивация выключателей задних электростеклоподъёмников");
            dictionary.Add(@"@P8878-CITACT", "Плюс (+) VAN на корпусе");
            dictionary.Add(@"@P8879-CITACT", "Плюс (+) VAN - комфорт");
            dictionary.Add(@"@P6607-CITACT", "Экономичный режим");

            dictionary.Add(@"@P113429-THESAU", "Впрыск топлива");
            dictionary.Add(@"@P54051-THESAU", "Впуск воздуха");
            dictionary.Add(@"@P1711-THESAU@\*", "Зажигание");
            dictionary.Add(@"@P9619-THESAU@\*", "Система очистки");
            dictionary.Add(@"@P111486-THESAU", "Охлаждение двигателя и климатическая установка");
            dictionary.Add(@"@P39717-THESAU", "Данные о движении");
            dictionary.Add(@"@P111487-THESAU", "Различные виды электрического питания и система иммобилайзера");
            dictionary.Add(@"@P106639-THESAU", "Данные группы электрооборудования-насоса");
            dictionary.Add(@"@P109209-THESAU", "Динамические виды информации");

            dictionary.Add(@"@P34627-THESAU", "Обороты двигателя");
            dictionary.Add(@"@P4525-THESAU@\*", "Напряжение аккумулятора");
            dictionary.Add(@"@P124840-THESAU", "Отключение впрыска топлива при замедлении");
            dictionary.Add(@"@P4545-THESAU", "Температура воды");
            dictionary.Add(@"@P98777-THESAU@\*", "нет");
            dictionary.Add(@"@P9077-THESAU@\*", "°C");
            dictionary.Add(@"@P4460-THESAU@\*", "об/мин");
            dictionary.Add(@"@P106701-THESAU@\*", "мс (миллисекунд)");

            dictionary.Add(@"@P124843-THESAU", "Cостояние дроссельной заслонки");
            dictionary.Add(@"@P1177-THESAU", "Холостой ход");
            dictionary.Add(@"@P9071-THESAU", "Средний газ");
            dictionary.Add(@"@P112875-THESAU", "Напряжение, соответствующее положению дроссельной заслонки");
            //dictionary.Add(@"@P4460-THESAU@\*", "Вольт");
            dictionary.Add(@"@P56982-THESAU", "Угол заслонки");
            dictionary.Add(@"@P32381-THESAU", "Давление коллектора");
            dictionary.Add(@"@P106700-THESAU@\*", "мБар");
            dictionary.Add(@"@P4548-THESAU", "Температура воздуха");
            //dictionary.Add(@"@T°", "°");
            dictionary.Add(@"@P4571-THESAU", "Cостояние электромагнитного клапана абсорбера паров бензина");
            dictionary.Add(@"@P92377-THESAU", "Неактивен");
            dictionary.Add(@"@P112031-THESAU", "Циклическое Соотношение Открытия электроклапана резервуара");
            dictionary.Add(@"@P14053-THESAU", "Нет");

            dictionary.Add(@"@P81592-THESAU", "Опережение угла зажигания");
            dictionary.Add(@"@P111757-THESAU@\*@T1/4", "время заряда катушки цилиндров 1/4");
            dictionary.Add(@"@P111757-THESAU@\*@T2/3", "время заряда катушки цилиндров 2/3");

            dictionary.Add(@"@P124850-THESAU", "Напряжение кислородного датчика на входе");
            dictionary.Add(@"@P124851-THESAU", "состояние кислородного датчика на входе");
            dictionary.Add(@"@P124852-THESAU", "напряжение кислородного датчика на выходе");
            dictionary.Add(@"@P87030-THESAU@\*", "состояние кислородного датчика на выходе");
            dictionary.Add(@"@P128676-THESAU", "состояние регулирования состава смеси");
            dictionary.Add(@"@P112874-THESAU", "контроль каталитического нейтрализатора");
            dictionary.Add(@"@P98567-THESAU", "Активный");
            dictionary.Add(@"@P98576-THESAU", "Замкнутый контур");
            dictionary.Add(@"@P93099-THESAU", "Неактивно");

            dictionary.Add(@"@P91477-THESAU@\*", "управление малой скоростью группы вентиляторов охлаждения двигателя");
            dictionary.Add(@"@P95482-THESAU@\*", "управление высокой скоростью группы вентиляторов  охлаждения двигателя");
            dictionary.Add(@"@P86968-THESAU", "заданная величина скорости вентилятора охлаждения двигателя");
            dictionary.Add(@"@P62146-THESAU", "разрешение на включение климатической установки");
            dictionary.Add(@"@P86855-THESAU@\*", "вход команды на включение климатической установки");

            dictionary.Add(@"@P58853-THESAU", "крутящий момент двигателя");
            dictionary.Add(@"@P12694-THESAU@\*", "Скорость автомобиля");
            dictionary.Add(@"@P105983-THESAU", "Включенная передача");
            dictionary.Add(@"@P4534-THESAU", "Время впрыска");
            dictionary.Add(@"@P10050-THESAU", "Нейтральная");
            dictionary.Add(@"@P43341-THESAU@\*", "Нм");
            dictionary.Add(@"@P16699-THESAU@\*", "км/ч");

            dictionary.Add(@"@P50143-THESAU", "управление реле питания");
            dictionary.Add(@"@P111491-THESAU", "запирание блока управления");
            dictionary.Add(@"@P20987-THESAU@\*", "компьютер  не блокирован");
            dictionary.Add(@"@P105556-THESAU", "состояние программирования кодируемого иммобилайзера");
            dictionary.Add(@"@P105560-THESAU@\*", "компьютер  согласован");
            dictionary.Add(@"@P112018-THESAU", "при передаче кода отпирания обнаружены проблемы");

            dictionary.Add(@"@P8335-THESAU@\+@P108919-THESAU@\*", "Скорость вращения электродвигателя ээлектронасоса");
            dictionary.Add(@"@P59533-THESAU", "Скорость поворота рулевого колеса");
            dictionary.Add(@"@P108824-THESAU", "Направление вращения рулевого колеса");
            dictionary.Add(@"@P9770-THESAU", "температура масла");
            dictionary.Add(@"@P61834-THESAU@\*@\+@P108919-THESAU@\*", "Ток электродвигателя электронасоса");
            dictionary.Add(@"@P32335-THESAU@\*@\+@P10169-THESAU@\*@\+@P108919-THESAU@\*", "Напряжение на клеммах электродвигателя электронасоса");
            dictionary.Add(@"@P14784-THESAU", "Информация о работающем двигателе");
            dictionary.Add(@"@P59531-THESAU", "Скорость автомобиля");
            dictionary.Add(@"@P37653-THESAU", "Правое");
            dictionary.Add(@"@P473-THESAU", "Да");
            dictionary.Add(@"@T°/s", "°/с");

            dictionary.Add(@"@P119345-THESAU", "напряжение питания компьютера системы ESP");
            dictionary.Add(@"@P107571-THESAU", "скорость левого переднего колеса");
            dictionary.Add(@"@P107570-THESAU", "скорость правого переднего колеса");
            dictionary.Add(@"@P99124-THESAU", "скорость заднего левого колеса");
            dictionary.Add(@"@P99123-THESAU", "скорость заднего правого колеса");
            dictionary.Add(@"@P59531-THESAU@\*", "скорость автомобиля");
            dictionary.Add(@"@P93098-THESAU", "Активно");
            dictionary.Add(@"@P98577-THESAU", "Замкнутый контур");

            dictionary.Add(@"@P4158-THESAU", "Уровень тормозной жидкости");
            dictionary.Add(@"@P110493-THESAU", "Износ тормозных колодок");
            dictionary.Add(@"@P108217-THESAU", "Достаточный");
            dictionary.Add(@"@P139380-THESAU", "Износ не определён");
            dictionary.Add(@"@TA", "A");
            dictionary.Add(@"@P37652-THESAU", "Правое");
            dictionary.Add(@"@P15630-CITACT", "Контроль компрессора кондиционера");
            dictionary.Add(@"@P13879-CITACT", "Управление средней скоростью вентилятора");
            dictionary.Add(@"P8904-CITACT", "Температура датчика испарителя");
            dictionary.Add(@"@P408-CITACT@\*", "°C");
            dictionary.Add(@"@P8909-CITACT", "Выход реле компрессора кондиционера");
            dictionary.Add(@"@P15537-CITACT", "Выход реле вентилятора");
            dictionary.Add(@"@P8904-CITACT", "Температура датчика испарителя");
            dictionary.Add(@"@P8907-CITACT", "Статус защиты испарителя от обледенения");
            dictionary.Add(@"@P6684-CITACT", "Средний расход топлива за поездку");
            dictionary.Add(@"@P9103-CITACT", "L/100 km");
            dictionary.Add(@"@P6685-CITACT", "Средний расход топлива");
            dictionary.Add(@"@P36096-CITACT", "Установленная скорость вращения вентилятора");
            dictionary.Add(@"@P15487-CITACT", "Запрос на деактивацию ESP");
            dictionary.Add(@"@P279-CITACT@\*-@P8873-CITACT", "Информация о вождении");
            dictionary.Add(@"@P15763-CITACT", @"Запрос на ""снежную"" программу автоматической коробки передач");
            dictionary.Add(@"@P16239-CITACT", "Парковочный тормоз");
            dictionary.Add(@"@P2323-CITACT", "Положение педали тормоза");
            dictionary.Add(@"@P4846-CITACT", "Информация о задней передаче");
            dictionary.Add(@"@P8471-citact", "Водительский ремень безопасности пристёгнут");
            dictionary.Add(@"@P8472-CITACT", "Пассажирский ремень безопасности пристёгнут");
            dictionary.Add(@"@P15602-CITACT", "Автоматическая коробка в положении парковки");
            dictionary.Add(@"@P8919-CITACT", "Давление в колёсах");
            dictionary.Add(@"@P19825-CITACT", "Статус датчика давления переднего левого колеса");
            dictionary.Add(@"@P19826-CITACT", "Статус датчика давления переднего правого колеса");
            dictionary.Add(@"@P19827-CITACT", "Статус датчика давления заднего правого колеса");
            dictionary.Add(@"@P19828-CITACT", "Статус датчика давления заднего левого колеса");
            dictionary.Add(@"@P8919-CITACT@\+@T-@\+@P19833-CITACT", "Показания датчика давления переднего левого колеса");
            dictionary.Add(@"@P8919-CITACT@\+@T-@\+@P19834-CITACT", "Показания датчика давления переднего правого колеса");
            dictionary.Add(@"@P8919-CITACT@\+@T-@\+@P19835-CITACT", "Показания датчика снижения давления заднего правого колеса");
            dictionary.Add(@"@P8919-CITACT@\+@T-@\+@P19836-CITACT", "Показания датчика снижения давления заднего левого колеса");
            dictionary.Add(@"@P19829-citact@\*1", "Оценка давления в переднем левом колесе");
            dictionary.Add(@"@P19830-citact@\*2", "Оценка давления в переднем правом колесе");
            dictionary.Add(@"@P19831-citact@\*3", "Оценка давления в заднем правом колесе");
            dictionary.Add(@"@P19832-citact@\*4", "Оценка давления в заднем левом колесе");
            dictionary.Add(@"@P439-CITACT", "Сигнализация");
            dictionary.Add(@"@P6617-citact", "Контроль освещения передних порогов");
            dictionary.Add(@"@P8945-CITACT", "Контроль задних противотуманных фар");
            dictionary.Add(@"@P4795-CITACT", "Контроль правых указателей поворота");
            dictionary.Add(@"@P4796-CITACT", "Контроль левых указателей поворота");
            dictionary.Add(@"@P5975-CITACT", "Команда на освещение задней двери");
            dictionary.Add(@"@P6618-CITACT", "Контроль внутреннего освещения");
            dictionary.Add(@"@P12713-CITACT", "Контроль звукового сигнала");
            dictionary.Add(@"@P8948-CITACT", "Контроль ближнего света");
            dictionary.Add(@"@P8950-CITACT", "Контроль дальнего света");
            dictionary.Add(@"@P6626-CITACT", "Запрос на внутреннее освещение");
            dictionary.Add(@"@P6633-CITACT", "Автоматический выбор режима освещения");
            dictionary.Add(@"@P15873-CITACT", "Наличие ошибки корректора фар");
            dictionary.Add(@"@P8966-CITACT", "Сигнал на выходе реле контроля лампы освещения багажника");
            dictionary.Add(@"@P8968-CITACT", "Сигнал на выходе реле контроля ламп указателей поворота");
            dictionary.Add(@"@P8969-CITACT", "Контроль боковых ламп");
            dictionary.Add(@"@P6638-CITACT", "Объёмный датчик взлома");
            dictionary.Add(@"@P5307-CITACT", "Деактивация защиты с помощью объёмного датчика");
            dictionary.Add(@"@P3306-CITACT", "Замки");
            dictionary.Add(@"@P6947-CITACT", "Замок капота");
            dictionary.Add(@"@P5506-CITACT", "Замок багажника");
            dictionary.Add(@"@P4818-CITACT", "Контроль закрытия замка");
            dictionary.Add(@"@P4819-CITACT", "Контроль открытия замка");
            dictionary.Add(@"@P9518-CITACT", "Контроль блокировки замка");
            dictionary.Add(@"@P23075-CITACT@\*", "Контроль закрытия замка багажника");
            dictionary.Add(@"@P8993-CITACT", "Ручка задней правой двери");
            dictionary.Add(@"@P8994-CITACT", "Ручка задней левой двери");
            dictionary.Add(@"@P23076-CITACT", "Запрос на открытие багажника");
            dictionary.Add(@"@P9008-CITACT", "Сигнал на выходе реле закрытия замка");
            dictionary.Add(@"@P9009-CITACT", "Сигнал на выходе реле открытия замка");
            dictionary.Add(@"@P9010-CITACT", "Сигнал на выходе реле блокировки замка");
            dictionary.Add(@"@P9011-CITACT", "Сигнал на выходе реле открытия замка багажника");
            dictionary.Add(@"@P9013-CITACT", "Информация об отпирании переднего правого замка");
            dictionary.Add(@"@P9012-CITACT", "Информация об отпирании переднего левого замка");
            dictionary.Add(@"@P9014-CITACT", "Контроль активации защиты ребёнка");
            dictionary.Add(@"@P9015-CITACT", "Контроль деактивации защиты ребёнка");
            dictionary.Add(@"@P9021-CITACT", "Показания измерительных приборов");
            dictionary.Add(@"@P7974-CITACT@\*", "л");
            dictionary.Add(@"@P6680-CITACT", "Ом");
            dictionary.Add(@"@P5669-CITACT@\*", "люкс");
            dictionary.Add(@"@P24202-CITACT", "Температура охлаждающей жидкости");
            dictionary.Add(@"@P6665-CITACT@\+@T-@\+@P6683-CITACT", "Инструментальная индикация");
            dictionary.Add(@"@P9135-CITACT", "Светодиод контроля защиты транспортного средства");
            dictionary.Add(@"@P22113-citact", "Индикатор низкого уровня омывающей жидкости");
            dictionary.Add(@"@P2322-CITACT", "Двери");
            dictionary.Add(@"@P4012-citact", "Контроль видимости");
            dictionary.Add(@"@P9157-CITACT", "Контроль заднего стеклоочистителя");
            dictionary.Add(@"@P8497-CITACT", "Контроль стеклоочистителей лобового стекла на высокой скорости");
            dictionary.Add(@"@P6697-CITACT", "Контроль стеклоочистителей лобового стекла на низкой скорости");
            dictionary.Add(@"@P8735-citact", "Контроль стеклоочистителей лобового стекла");
            dictionary.Add(@"@P9158-CITACT", "Контроль заднего стеклоомывателя");
            dictionary.Add(@"@P6699-CITACT", "Контроль переднего стеклоомывателя");
            dictionary.Add(@"@P6700-CITACT", "Контроль омывателя фар");
            dictionary.Add(@"@P4860-CITACT", "Информация о парковочном положении заднего стеклоочистителя");
            dictionary.Add(@"@P4861-CITACT", "Информация о парковочном положении переднего стеклоочистителя");
            dictionary.Add(@"@P5313-CITACT", "Состояние элементов панели управления");
            dictionary.Add(@"@P5294-CITACT", "Нажатие кнопки обогрева заднего стекла");
            dictionary.Add(@"@P5292-CITACT", "Нажатие кнопки аварийного сигнала");
            dictionary.Add(@"@P5293-CITACT", "Нажатие кнопки кондиционера");
            dictionary.Add(@"@P5296-CITACT", "Уровень яркости подсветки панели управления");
            dictionary.Add(@"@P14602-CITACT", "Кнопка контроля подвески");
            dictionary.Add(@"@P1519-CITACT@\*", "%");
            dictionary.Add(@"@P1523-CITACT@\*", "В");
            dictionary.Add(@"@P3129-CITACT", "Переменные параметры");
            dictionary.Add(@"@P19737-CITACT", "Температура охлаждающей жидкости");
            dictionary.Add(@"@P7902-CITACT", "Напряжение питания");
            dictionary.Add(@"@P27289-CITACT", "Скорость воздушной турбины");
            dictionary.Add(@"@P10655-CITACT", "Скорость насоса-дозатора");
            dictionary.Add(@"@P27287-CITACT", "Скорость свечей зажигания");
            dictionary.Add(@"@P10115-CITACT", "Состояния агрегатов");
            dictionary.Add(@"@P27291-CITACT", "Состояние дополнительного подогрева");
            dictionary.Add(@"@P3218-CITACT", "Подогрев");
            dictionary.Add(@"@P10661-CITACT", "Наличие возгорания");
            dictionary.Add(@"@P28098-CITACT", "Состояние лампы накаливания");
            dictionary.Add(@"@P28096-CITACT", "Состояние топливного насоса");
            dictionary.Add(@"@P3157-CITACT", "Воздушная турбина");
            dictionary.Add(@"@P3159-CITACT", "Водяной насос");
            dictionary.Add(@"@P10039-CITACT", "Уровень сброса нагрузки");
            dictionary.Add(@"@P12460-CITACT", "Нет сброса нагрузки");
            dictionary.Add(@"@P20121-CITACT", "Сброс нагрузки свечей предварительного подогрева");
            dictionary.Add(@"@P22037-CITACT", "Принудительный сброс нагрузки вентилятора на средней скорости");
            dictionary.Add(@"@P22038-CITACT", "Принудительный сброс нагрузки вентилятора на низкой скорости");
            dictionary.Add(@"@P12639-CITACT", "Принудительный сброс нагрузки защиты от обледенения");
            dictionary.Add(@"@P12640-CITACT", "Сброс нагрузки защиты от обледенения");
            dictionary.Add(@"@P12641-CITACT", "Сброс нагрузки частичного подогрева");
            dictionary.Add(@"@P12642-CITACT", "Сброс нагрузки полного подогрева");
            dictionary.Add(@"@P12643-CITACT", "Сброс нагрузки нагнетателя кондиционера");
            dictionary.Add(@"@P12644-CITACT", "Сброс нагрузки компрессора кондиционера");

            dictionary.Add(@"@P6616-CITACT", "Освещение");
            dictionary.Add(@"@P6665-CITACT@\+@T-@\+@P10783-CITACT", "Показания измерительных приборов");
            dictionary.Add(@"@P3934-CITACT", "Заводской режим");
            dictionary.Add(@"@P15638-CITACT", @"Наличие ""+"" на аксессуарах");
            dictionary.Add(@"@P15639-CITACT", @"Наличие ""+"" на зажигании");
            dictionary.Add(@"@P8888-CITACT", "Напряжение возбуждения на генераторе переменного тока");
            dictionary.Add(@"@P8898-CITACT", "Отсечка напряжения на топливном насосе");
            dictionary.Add(@"@P8899-CITACT", "Статус на выходе реле топливного насоса");
            dictionary.Add(@"@P8900-CITACT", "Управление воздушным насосом");
            dictionary.Add(@"@P8901-CITACT", "Статус на выходе реле воздушного/топливного насоса дизельного двигателя");
            dictionary.Add(@"@P20086-CITACT", "Правильность показаний датчиков давления в колёсах");
            dictionary.Add(@"@P8965-CITACT", "Сигнал на выходе реле контроля лампы внутреннего света");
            dictionary.Add(@"@P3837-CITACT", "Предупреждение о давлении масла");
            dictionary.Add(@"@P16232-CITACT", "Вода в дизеле");
            dictionary.Add(@"@P6668-CITACT", "Уровень масла");
            dictionary.Add(@"@P3830-CITACT", "Уровень топлива (брутто)");
            dictionary.Add(@"@P18875-CITACT", "Температура воздуха снаружи");
            dictionary.Add(@"@P6679-CITACT", "°С");
            dictionary.Add(@"@P6671-CITACT", "Температура масла");
            dictionary.Add(@"@P12449-CITACT", "Уровень топлива (фильтрованный)");
            dictionary.Add(@"@P6672-CITACT", "Отображаемый уровень топлива");
            dictionary.Add(@"@P6673-CITACT", "Количество пройденных сервисов");
            dictionary.Add(@"@P6676-CITACT", "Сопротивление датчика уровня топлива");
            dictionary.Add(@"@P15489-CITACT", "Предупреждение о низком уровне тормозной жидкости");
            dictionary.Add(@"@P6431-citact@\+@T-@\+@P10054-CITACT", "Уровень сумерек датчика солнечного света");
            dictionary.Add(@"@P2016-CITACT", "Данные пройденного пути");
            dictionary.Add(@"@P4840-CITACT", "Общий пробег");
            dictionary.Add(@"@P6687-CITACT", "Сглаженное значение расхода топлива");
            dictionary.Add(@"@P6691-CITACT", "Контроль поднятия заднего правого стекла");
            dictionary.Add(@"@P6692-CITACT", "Контроль поднятия заднего левого стекла");
            dictionary.Add(@"@P6693-CITACT", "Контроль опускания заднего правого стекла");
            dictionary.Add(@"@P6694-CITACT", "Контроль опускания заднего левого стекла");
            dictionary.Add(@"@P12059-CITACT", "Кнопка сброса одометра");

            dictionary.Add(@"@P90043-THESAU", "Электропитание");
            dictionary.Add(@"@P111636-THESAU", "Подано");
            dictionary.Add(@"@P1735-THESAU@\*", "Обогрев и вентиляция");
            dictionary.Add(@"@P110879-THESAU", "Путевые данные");
            dictionary.Add(@"@P33453-THESAU@\*100", "л/100км");
            dictionary.Add(@"@P24901-THESAU@\*", "км");
            dictionary.Add(@"@P1948-THESAU", "Крышка бака");
            dictionary.Add(@"@P12350-THESAU", "Отсутствует");
            dictionary.Add(@"@P108909-THESAU", "Cостояние датчиков и вентилятора печки");
            dictionary.Add(@"@P108879-THESAU", @"Напряжение ""+"" VAN");
            dictionary.Add(@"@P108901-THESAU@\*", "Температору воздуха левого воздуховода");
            dictionary.Add(@"@P108902-THESAU@\*", "Температура воздуха правого воздуховода");
            dictionary.Add(@"@P105487-THESAU@\*", "Состояние исполнительных элементов системы отопления");
            dictionary.Add(@"@P109641-THESAU", "Очень горячий");
            dictionary.Add(@"@P108235-THESAU", "Ноги");
            dictionary.Add(@"@P41944-THESAU", "Средний");
            dictionary.Add(@"@P108913-THESAU@\*@\+@P37652-THESAU", "Состояние левой системы смешивания");
            dictionary.Add(@"@P108882-THESAU", "Состояние правой системы смешивания");
            dictionary.Add(@"@P108881-THESAU", "Распределение");
            dictionary.Add(@"@P108884-THESAU", "Состояние рециркуляции");
            dictionary.Add(@"@P120093-THESAU@\*", "Контроль вентилятора");
            dictionary.Add(@"@P111700-THESAU", "Показания датчика температуры испарителя");
            dictionary.Add(@"@P120094-THESAU", "Состояние устройства защиты хладагента от замерзания");
            dictionary.Add(@"@P110797-THESAU@\*1", "Основные параметры 1");
            dictionary.Add(@"@P13563-THESAU@\*@\*", "бар");
            dictionary.Add(@"@P85004-THESAU@\*", "мм3/ход поршня");
            dictionary.Add(@"@P28890-THESAU@\*", "мг/импульс");
            dictionary.Add(@"@P106761-THESAU@\*1@\*4", "Напряжение на инжекторах 1 и 4");
            dictionary.Add(@"@P106761-THESAU@\*2@\*3", "Напряжение на инжекторах 2 и 3");
            dictionary.Add(@"@P110797-THESAU@\*2", "Основные параметры 2");
            dictionary.Add(@"@P107237-THESAU", "Информация об электросистеме");
            dictionary.Add(@"@P105988-THESAU@\*1@\+@T(@\+@P44699-THESAU@\+@T)", "Напряжение питания датчиков распредвалов");
            dictionary.Add(@"@P106180-THESAU", "Информация о сажевом фильтре (FAP)");
            
            // =================== Model names =================
            dictionary.Add(@"@P27373-CITTD", "Evasion");
            dictionary.Add(@"@P16408-CITTD", "C8");
            dictionary.Add(@"@P20015-CITTD", "C4");
            // =================================================

            dictionary.Add(@"@P31100-CITACT", "Данные присадки сажевого фильтра");
            dictionary.Add(@"@P2366-CITACT", "бар");
            dictionary.Add(@"@P6014-CITACT", "Низкий уровень присадки");
            dictionary.Add(@"@P17140-CITACT", "Крышка топливного бака");
            dictionary.Add(@"@P31106-CITACT@\+@P21305-CITACT",
                "Общий объём присадки, потреблённый в сажевом фильтре (счётчик 1)");
            dictionary.Add(@"@P10309-CITACT@\*", "грамм");
            dictionary.Add(@"@P17142-CITACT", 
                "Общий объём присадки, инжектированный из бака с присадкой (счётчик 2)");
            dictionary.Add(@"@P105964-THESAU", "Информация о движении");
            dictionary.Add(@"@Tmb", "мб");
            dictionary.Add(@"@P19420-CITACT", "Информация - вождение");
            dictionary.Add(@"@P22223-CITACT@\*(1)", "Задняя передача включена (1)");
            dictionary.Add(@"@P23184-CITACT@\*(1)", "АКПП в парковочном положении (1)");
            dictionary.Add(@"@P25705-CITACT", "Auto / Sensodrive gearbox lever position seen by engine relay unit (BSM)");
            dictionary.Add(@"@P25705-CITACT", "Рычаг переключения АКПП / КПП Sensodrive распознан модулем управления двигателем (BSM)");
            dictionary.Add(@"@P16124-CITACT", "Details of the electric pump unit");
            dictionary.Add(@"@P16124-CITACT", "Информация об электронасосе");
            dictionary.Add(@"@P18701-CITACT", "Наличие информации об угле поворота рулевого колеса");
            dictionary.Add(@"@P6668-CITACT@\+@T(1)", "Измеренный уровень масла (1)");
            dictionary.Add(@"@P6671-CITACT@\+@T(1)", "Температура масла (1)");
            dictionary.Add(@"@P22559-CITACT@\*(1)", "Предупреждение о давлении масла (1)"); // Oil pressure warning (1)
            dictionary.Add(@"@P22577-CITACT", "Скорость вращения рулевого колеса");
            dictionary.Add(@"@P18706-CITACT", "Направление вращения рулевого колеса");
            dictionary.Add(@"@P18707-CITACT", "Температура электронасоса рулевого управления");*/
            //@P18710-CITACT Ток электронасоса рулевого управления
            //@P16126-CITACT Напряжение на клеммах электронасоса рулевого управления
            //@P8876-CITACT Информация о работающем двигателе
            //@P18708-CITACT Скорость вращения электродвигателя электронасоса рулевого управления
            //@P18709-CITACT Наличие сигнала о скорости 
            //@P5423-CITACT Скорость транспортного средства
            //@P21933-THESAU

            //dictionary.Add(@"", "");
        }

        /*public static bool Contains(string identifyer)
        {
            if (dictionary.ContainsKey(identifyer))
            {
                return true;
            }
            return false;
        }

        public static string Resolve(string val)
        {
            if (String.IsNullOrEmpty(val) || val[0] != '@') // already resolved
            {
                return val;
            }
            return dictionary[val];
        }*/
    }
}
