using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter {

	public class Notification {

		public string m_title = "null";
		public string m_message = "null";
	}

	private Dictionary<int, List<Notification>> m_notifications = new Dictionary<int, List<Notification>>();

	public void AddNotification (int turnNumber, string title, string message)
	{
		Notification n = new Notification ();
		n.m_title = title;
		n.m_message = message;

		if (m_notifications.ContainsKey (turnNumber)) {

			List<Notification> notifications = m_notifications [turnNumber];
			notifications.Add (n);
			m_notifications [turnNumber] = notifications;

		} else {
			List<Notification> newNotification = new List<Notification> ();
			newNotification.Add (n);
			m_notifications.Add (turnNumber, newNotification);
		}
	}

	public Dictionary<int, List<Notification>> notifications {get{ return m_notifications;}}
}
