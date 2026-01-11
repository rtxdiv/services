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
    `user_id` TEXT NOT NULL,
    `service_name` TEXT NOT NULL,
    `query` TEXT NOT NULL,
    `contact` TEXT NOT NULL,
    `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `status` BOOLEAN NULL DEFAULT NULL,
    `status_text` TEXT NOT NULL,
    `user_noti` BOOLEAN NOT NULL DEFAULT false,
    `admin_noti` BOOLEAN NOT NULL DEFAULT true,
    PRIMARY KEY (`id`)
)
COLLATE='utf8mb4_0900_ai_ci';