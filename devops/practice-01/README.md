# Первое практическое занятие по Docker

- Автор: Борисов Александр
- Группа: ИТ-9.24-3-ДН

## Комманды из пункта №7
> Запустите ещё один контейнер из student-site:v2. Назовите его my-copy.
```bash
docker run -d --name my-copy -p 127.0.0.1:8052:80 student-site:v2
```
> Остановите только my-site. Обновите обе страницы через Ctrl+F5: сайт на 8051 не загружается, а на 8052 продолжает работать.
```bash
docker stop my-site
```
![alt text](https://github.com/borborisovai/dso-kt2/blob/main/screenshots/1.png?raw=true)

> Выполните docker `ps -a`. Сделайте скриншот: my-site имеет статус Exited, my-copy — Up. Сохраните также скриншот сайта на 8052.

![alt text](https://github.com/borborisovai/dso-kt2/blob/main/screenshots/2.png?raw=true)

> Запустите my-site снова. Убедитесь, что обе страницы доступны.
```bash
docker start my-site
```
![alt text](https://github.com/borborisovai/dso-kt2/blob/main/screenshots/3.png?raw=true)


# Остальные скриншоты

> Лог lab-web с запросом /missing и кодом 404.

![alt text](https://github.com/borborisovai/dso-kt2/blob/main/screenshots/5.png?raw=true)

> Ваш сайт на 8051 с надписью «Версия 2».

![alt text](https://github.com/borborisovai/dso-kt2/blob/main/screenshots/4.png?raw=true)
