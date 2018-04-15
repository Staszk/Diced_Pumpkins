/* Author: Parker Staszkiewicz, Lead Programmer
 * Project: Cookbook (Working Title)
 * Script Purpose: 
 * 		Controls Game logic
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour 
{
	// References to class objects
	private BookManager _bookManager;
	private AudioManager _audioManager;
	private WorldCanvasManager _worldCanvasManager;
	private InventoryUIManager _inventoryUIManager;
	private InventoryManager _inventoryManager;
	private SceneMenuUIManager _sceneMenuUIManager;
	private JSONReadManager _jsonManager;
	private SceneManager _sceneManager;
	private EventSystem _eventSystem;
	private InputSystem _inputSystem;
	private InputTranslator _inputTranslator;

	// Public data to be passed
	[Header("Book Manager Field")]
	public GameObject bookObject;
	public Canvas bookCanvas;

	[Header("World Canvas Manager Field")]
	public Canvas worldCanvas;
	public TextMeshProUGUI textField;
	public Image scoreImage;
	public Sprite[] sprites;

	[Header("Food Inventory Manager Field")]
	public Canvas inventoryCanvas;

	[Header("Scene Manager Field")]
	public Camera mainCamera;

	[Header("Scene UI Manager Field")]
	public Image menuPanel;
	public Image icon;

	private void Awake ()
	{
		_eventSystem = new EventSystem();
		_eventSystem.Initialize();

		_inputSystem = new InputSystem();

		_inputTranslator = new InputTranslator();

		_audioManager = new AudioManager();
		_audioManager.Initialize ();

		_bookManager = new BookManager(bookObject, bookCanvas);

		_worldCanvasManager = new WorldCanvasManager(worldCanvas, textField, scoreImage, sprites);
		_worldCanvasManager.Initialize();

		_inventoryManager = new InventoryManager();
		_inventoryManager.Initialize();

		_inventoryUIManager = new InventoryUIManager(inventoryCanvas);

		_sceneMenuUIManager = new SceneMenuUIManager(menuPanel, icon);

		_jsonManager = new JSONReadManager();
		_jsonManager.Initialize();
		_jsonManager.LoadFile("FoodObjectFile");
		_jsonManager.LoadFile("SceneObjectFile");

		_sceneManager = new SceneManager(mainCamera);
		_sceneManager.Initialize();

		// Assign EventTypes to EventListeners
		// and Add them to the EventSystem's list
		_eventSystem.addListener(EventType.KEY_PRESSED, _inputTranslator);

		_eventSystem.addListener(EventType.BOOK_INTERACT, _bookManager);
		_eventSystem.addListener(EventType.NUM_EVENT_TYPES, _bookManager);

		_eventSystem.addListener(EventType.BOOK_INTERACT, _audioManager);
		_eventSystem.addListener(EventType.PAGE_FLIPPED, _audioManager);
		_eventSystem.addListener(EventType.KNIFE_CUT, _audioManager);

		_eventSystem.addListener(EventType.POINTS_SCORED, _worldCanvasManager);

		_eventSystem.addListener(EventType.FOOD_INVENTORY, _inventoryUIManager);
		_eventSystem.addListener(EventType.SCREEN_PRESSED, _inventoryUIManager);

		_eventSystem.addListener(EventType.SCENE_MENU, _sceneMenuUIManager);
		_eventSystem.addListener(EventType.SCREEN_PRESSED, _sceneMenuUIManager);
	}

	private void Update ()
	{
		_inputSystem.Update ();
	}

	// Function to be called in Book.cs UnityEvent OnFlip
	public void PageFlipped()
	{
		_eventSystem.fireEvent (new PageFlippedEvent ());
	}

	// Function to be called on UI Canvas Button
	public void InventoryButtonPressed()
	{
		_eventSystem.fireEvent(new FoodInventoryEvent());
	}

	public void SceneMenuPressed()
	{
		_eventSystem.fireEvent(new SceneMenuEvent());
	}

	public void CallBook()
	{
		_eventSystem.fireEvent(new BookInteractEvent());
	}

	public void TrashFood()
	{
		_sceneManager.RemoveSceneFood(-1);
	}
}
