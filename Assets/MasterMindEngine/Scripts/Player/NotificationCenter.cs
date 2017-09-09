using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter {

	public class Notification : ISubject {

		public string m_title = "null";
		public string m_message = "null";
		public EventLocation m_location = EventLocation.None;
		public int m_missionID = -1;

		public void AddObserver (IObserver observer){}

		public void RemoveObserver (IObserver observer){}

		public void Notify (ISubject subject, GameEvent thisGameEvent){}
	}

	private Dictionary<int, List<Notification>> m_notifications = new Dictionary<int, List<Notification>>();
	private Dictionary<int, List<Notification>> m_notificationsByMissionID = new Dictionary<int, List<Notification>>();

	public void AddNotification (int turnNumber, string title, string message, EventLocation location, bool alertPlayer, int missionID)
	{
		Notification n = new Notification ();
		n.m_title = title;
		n.m_message = message;
		n.m_location = location;
		n.m_missionID = missionID;

		if (m_notifications.ContainsKey (turnNumber)) {

			List<Notification> notifications = m_notifications [turnNumber];
			notifications.Add (n);
			m_notifications [turnNumber] = notifications;

		} else {
			List<Notification> newNotification = new List<Notification> ();
			newNotification.Add (n);
			m_notifications.Add (turnNumber, newNotification);
		}

		if (missionID >= 0) {
			
			if (m_notificationsByMissionID.ContainsKey (missionID)) {

				List<Notification> notifications = m_notificationsByMissionID [missionID];
				notifications.Add (n);
				m_notificationsByMissionID [missionID] = notifications;

			} else {
				List<Notification> newNotification = new List<Notification> ();
				newNotification.Add (n);
				m_notificationsByMissionID.Add (missionID, newNotification);
			}
		}

		if (alertPlayer) {
			GameController.instance.Notify (n, GameEvent.Player_NotificationReceived);
		}
	}

	public Dictionary<int, List<Notification>> notifications {get{ return m_notifications;}}
	public Dictionary<int, List<Notification>> notificationsByMissionID {get{ return m_notificationsByMissionID;}}
}
