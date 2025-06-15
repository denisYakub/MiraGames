# MiraGames Тестовое задание: Admin Dashboard (Minimal Slice)
## Запуск
1) Для начала пропишите свою строку подключения к BD в ..\MiraGames\MiraGames.Server\appsettings.json, строка PostgresConnection.
2) Перейдите в папку MiraGames.Server проекта.
3) Откройте консоль и напишите команду dotnet run (для работы необходимо установить .NET SDK: https://dotnet.microsoft.com/ru-ru/download и Node.js: https://nodejs.org/en).
4) После успешного запуска вы можете перейти по https://localhost:49989/ и попадете на клиентскую часть, а так же перейдя по http://localhost:5276/swagger/ вы сможете ознакомиться с Api сервера (Проверьте, чтоб порты были свободный).
5) Чтоб перейти на доску зарегестрируйтесь (пример логин\пароль: denis@gmail.com/denis123)
## Скрины работы
1) Swagger UI
![image](https://github.com/user-attachments/assets/34070dc3-6662-4204-9548-7bc628a16e6f)
2) Vite + React
![image](https://github.com/user-attachments/assets/ca86a6b4-8d31-45bd-8d7e-a304ba50a7b7)
## Текст задания
1. Цель
Показать, как вы:
• проектируете REST API и структуру данных на ASP.NET Core 8
• пишете чистый, читаемый .NET‑код
• связываете бэкенд с простым React‑фронтом
• сопровождаете работу понятным README

Ожидаемое время: ≈ 1,5 рабочего дня (около 12 ч чистого времени)
2. Обязательная часть (Must Have)
2.1 Backend /api
Стек
ASP.NET Core 8 (minimal API)
Хранилище
SQLite (In‑Memory или файл)
Сиды
3 клиента, 5 платежей, курс 10


Эндпойнты:
• POST /auth/login — email+pwd → { token:"demo" }
• GET  /clients — все клиенты
• GET  /payments?take=5 — последние N платежей
• GET  /rate — текущий курс
• POST /rate — обновить курс

2.2 Frontend /
Стек: Vite + React
• /login — форма, сохраняет token в localStorage
• /dashboard — таблица «Клиенты» (Name, Email, BalanceT) + блок «Курс токенов» (показ + форма изменить)

2.3 README
• Запуск двумя командами:
• dotnet run        # API на :5000
• npm run dev       # React на :5173
• Данные для входа: admin@mirra.dev / admin123
• Пара примеров curl / Postman
3. Бонус‑часть (Nice to Have)
• Docker Compose (PostgreSQL + миграции, 3 контейнера)
• Настоящий JWT (HS256), middleware, refresh‑токены
• Полный CRUD /clients и редактор меток
• История платежей на UI
• Тесты / CI pipeline
4. Что прислать
• Ссылка на GitHub‑репозиторий
• Опционально: короткий скринкаст или деплой‑ссылка
5. Критерии оценки
• Запускается по README без ошибок — 25 %
• Чистота и структура кода (.NET) — 25 %
• Корректность работы API — 20 %
• Связка фронт ↔ бэк — 20 %
• Реализованные бонусы — до 10 %
6. Дедлайн и связь
Базовая часть — дата согласуется. Если нужно больше времени — предупредить заранее.
