# Drag-and-Drop Mechanics Demo

Это демонстрационное приложение демонстрирует реализацию механики Drag-and-Drop в Unity. Пользователь может перемещать объекты по экрану, а затем отпускать их, чтобы они падали под действием гравитации. Также доступен функционал прокрутки сцены для перемещения по комнате.

## Установка и запуск

Для запуска приложения выполните следующие шаги:

1. Клонируйте репозиторий:

    ```bash
    git clone https://github.com/DigitalCatharsis/DragAndDrop_Test.git
    ```

2. Откройте проект в Unity.

3. Запустите сцену `MainScene`.

## Основные функции

- **Drag-and-Drop**: Переместите объект по экрану и отпустите его, чтобы он упал под воздействием гравитации.
- **Прокрутка сцены**: Используйте мышь или сенсорный экран для перемещения по комнате.
- **Визуальные эффекты и звук**: Реализация анимаций и звуков при сборе объектов.

## Структура проекта

### Скрипты (`Assets/Scripts`)

- **SpriteColorChanger.cs**: Изменение прозрачности спрайтов.
- **MenuButtonController.cs**: Управление кнопками главного меню.
- **TempRestart.cs**: Перезапуск текущей сцены.
- **PlayerReaction.cs**: Реакции [данные удалены] на различные события в игре.
- **AppleReceiver.cs**: Обработка взаимодействий с объектами типа "яблоки".
- **CameraSizer.cs**: Настройка размера камеры под разрешение экрана.
- **DragManager.cs**: Управление механизмом Drag-and-Drop.
- **GameLogic.cs**: Основная логика работы игры.
- **CameraMover.cs**: Управление движением камеры.