CREATE TABLE `services` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `name` TEXT NOT NULL,
    `description` TEXT NOT NULL,
    `requirements` TEXT NOT NULL,
    `image` TEXT NULL DEFAULT NULL,
    `visible` BOOLEAN NOT NULL DEFAULT true,
    PRIMARY KEY (`id`)
)
COLLATE='utf8mb4_0900_ai_ci';

CREATE TABLE `requests` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `service` INT NOT NULL,
    `request` TEXT NOT NULL,
    `contact` TEXT NOT NULL,
    `created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `status` BOOLEAN NULL DEFAULT NULL,
    `user_noti` BOOLEAN NOT NULL DEFAULT false,
    `admin_noti` BOOLEAN NOT NULL DEFAULT true,
    PRIMARY KEY (`id`)
)
COLLATE='utf8mb4_0900_ai_ci';