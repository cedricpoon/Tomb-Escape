using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox
{
	public string message { get; private set; }
	public float duration { get; private set; }
	public static bool HasMessageBoxOnScreen
	{
		get { return present != null; }
	}

	static MessageBox present;

	public delegate void OnFaded();

	static readonly Font FONT_FACE = Resources.Load<Font>("Fonts/IndieFlower");
	static readonly Vector2 ScalerRefResolution = new Vector2(500, 100);
	const int FONT_SIZE = 10;

	public const float DURATION_SHORT = 1, DURATION_LONG = 3, DURATION_FOREVER = -1, DURATION_NONE = 0;

	readonly MonoBehaviour mono;
	float y;
	Text _text;
	RectTransform _rect;
	OnFaded faded;
	Color color = Color.white;

	public static void Show(MonoBehaviour mono)
	{
		Show(mono, " ");
	}

	public static void Show(MonoBehaviour mono, string message)
	{
		Show(mono, message, DURATION_SHORT);
	}

	public static void Show(MonoBehaviour mono, string message, float duration)
	{
		Show(mono, message, duration, 0);
	}

	public static void Show(MonoBehaviour mono, string message, float duration, float y)
	{
		new MessageBox(mono, message, duration, y).Show();
	}

	public static void Clear()
	{
		// Clear current
		present = null;
	}

	public MessageBox(MonoBehaviour mono) : this(mono, " ") { }

	public MessageBox(MonoBehaviour mono, string message) : this(mono, message, DURATION_SHORT) { }

	public MessageBox(MonoBehaviour mono, string message, float duration) : this(mono, message, duration, 0) { }

	public MessageBox(MonoBehaviour mono, string message, float duration, float y)
	{
		this.mono = mono;
		this.message = message;
		this.duration = duration;
		this.y = y;
	}

	public void Show()
	{
		mono.StartCoroutine(ShowCoroutine());
	}

	public void ShowInstantly()
	{
		mono.StartCoroutine(ShowCoroutine(true));
	}

	public MessageBox SetColor(Color color)
	{
		this.color = color;
		return this;
	}

	public void Dispose()
	{
		DisposeCoroutine(duration);
	}

	public void DisposeNow()
	{
		DisposeCoroutine(0);
	}

	public MessageBox SetFadedEventHandler(OnFaded faded)
	{
		this.faded = faded;
		return this;
	}

	void Showing(bool instantly = false)
	{
		// Update object
		if (!instantly)
			present = this;

		// Create canvas
		GameObject canvas = new GameObject("MessageBox");
		canvas.AddComponent<Canvas>();
		canvas.AddComponent<CanvasScaler>();

		MakeCanvas(
			canvas.GetComponent<Canvas>()
		);

		MakeCanvasScaler(
			canvas.GetComponent<CanvasScaler>()
		);

		// Create Text
		GameObject text = new GameObject("Content");
		text.transform.SetParent(canvas.transform);
		text.AddComponent<Text>();

		MakeRectTransform(
			text.GetComponent<RectTransform>(),
			ScalerRefResolution * 0.8f
		);
		MakeText(text.GetComponent<Text>(), message);

		// Blinding
		this._text = text.GetComponent<Text>();
		this._rect = text.GetComponent<RectTransform> ();
	}

	void Disposing()
	{
		GameObject msgBox = GameObject.Find("MessageBox");
		if (msgBox != null)
			Object.Destroy(msgBox);

		// Update current object
		present = null;

		// Call faded event
		if (faded != null)
			faded();
	}

	IEnumerator Fade(float _from, float _to, OnFaded faded)
	{
		for (float i = _from; i > _to; i -= 0.5f) {
			_rect.localPosition = new Vector3 (_rect.localPosition.x, i, 0);
			yield return new WaitForSeconds (0.01f);
		}
		faded();
	}

	IEnumerator FadeColor(float _from, float _to)
	{
		if (_from < _to) {
			for (float i = _from; i < _to + 0.1f; i += 0.1f) {
				_text.color = new Color (
					_text.color.r,
					_text.color.g,
					_text.color.b,
					i
				);
				yield return new WaitForSeconds (0.01f);
			}
		} else {
			for (float i = _from; i > _to; i -= 0.1f) {
				_text.color = new Color (
					_text.color.r,
					_text.color.g,
					_text.color.b,
					i
				);
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	IEnumerator ShowCoroutine(bool instantly = false)
	{
		// Loop until no MessageBox is displaying
		while (present != null && !instantly)
			yield return null;

		Showing(instantly);
		mono.StartCoroutine(FadeColor(0, 1));
		mono.StartCoroutine(Fade(y + 5, y, Dispose));
	}

	void DisposeCoroutine(float duration)
	{
		if (duration >= 0) {
			new WaitForSecondsIEnum (duration, delegate(object[] objects) {
				mono.StartCoroutine(FadeColor(1, 0));
				mono.StartCoroutine (Fade (y, y - 5, Disposing));
			}).Run (mono);
		}
	}

	void MakeCanvas(Canvas canvas)
	{
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
	}

	void MakeCanvasScaler(CanvasScaler canvasScaler)
	{
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvasScaler.referenceResolution = ScalerRefResolution;
	}

	void MakeRectTransform(RectTransform rectTransform, Vector2 sizeDelta)
	{
		rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
		rectTransform.localPosition = Vector3.zero;
		rectTransform.localScale = Vector3.one;
		rectTransform.sizeDelta = sizeDelta;
	}

	void MakeText(Text text, string message)
	{
		text.text = message;
		text.color = new Color(color.r, color.g, color.b, 0);
		text.font = FONT_FACE;
		text.alignment = TextAnchor.MiddleCenter;
		text.fontSize = FONT_SIZE;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
	}
}