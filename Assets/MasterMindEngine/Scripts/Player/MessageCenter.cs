using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter
{

	public class Message
	{
		public string message = "";
		public int m_turnStamp = -1;
	}

	public class Thread 
	{
		public int m_actorID = -1; // whom the player is communicating with

		public List<Message> m_messages = new List<Message>();

		public void AddMessage (Message newMessage)
		{
			m_messages.Add (newMessage);
		}
	}

	private Dictionary<int, Thread> m_threads = new Dictionary<int, Thread>(); //actorID, Thread between player and actor

	public void CreateThread (int actorID)
	{
		if (!m_threads.ContainsKey (actorID)) {

			Thread newThread = new Thread ();
			newThread.m_actorID = actorID;
			m_threads.Add (actorID, newThread);

		} else {

			Debug.Log ("Thread with this actorID already exists");
		}
	}

	public Thread GetThread (int actorID)
	{
		if (!m_threads.ContainsKey (actorID)) {

			return m_threads [actorID];

		} else {

			Debug.Log ("Thread with this actorID doesn't exist");
		}

		return null;
	}
}
