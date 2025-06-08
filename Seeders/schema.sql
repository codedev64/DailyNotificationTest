CREATE TABLE IF NOT EXISTS Users (
  user_id VARCHAR(255) PRIMARY KEY,
  platform ENUM('ios', 'android') NOT NULL,
  notifications_enabled BOOLEAN DEFAULT FALSE,
  notification_time TIME NULL
);

INSERT INTO Users (user_id, platform, notifications_enabled, notification_time) VALUES
('user1', 'android', true, '12:00:00'),
('user2', 'ios', false, NULL),
('user3', 'android', true, '15:00:00');