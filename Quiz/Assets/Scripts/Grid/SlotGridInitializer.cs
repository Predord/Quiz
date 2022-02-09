using System;
using System.Collections.Generic;
using UnityEngine;
using Quiz.Level;
using Quiz.UI;

using Random = UnityEngine.Random;

namespace Quiz.Grid 
{
    public class SlotGridInitializer : MonoBehaviour, IBundle
    {
        public int BundleSize
        {
            get
            {
                return slotBundleData.Length;
            }
        }

        [SerializeField]
        private Transform slotPrefab;

        [SerializeField]
        private SlotBundleData[] slotBundleData;

        private IRandomizer randomizer;
        private ITextSetter textSetter;
        private IGridData grid;
        private Transform _transform;

        private List<int>[] inValidChoices;

        private void Awake()
        {
            _transform = transform;
            randomizer = GetComponent<IRandomizer>();
            textSetter = GetComponent<ITextSetter>();
            grid = GetComponent<IGridData>();
        }

        private void Start()
        {
            GenerateInValidChoices();

            CreateGridRows();
        }

        private void OnEnable()
        {
            grid.OnNewGridCreate += CreateGridRows;
        }

        private void OnDisable()
        {
            grid.OnNewGridCreate -= CreateGridRows;
        }

        private void GenerateInValidChoices()
        {
            if (inValidChoices != null)
            {
                for (int i = 0; i < inValidChoices.Length; i++)
                {
                    inValidChoices[i].Clear();
                }
            }
            else
            {
                inValidChoices = new List<int>[slotBundleData.Length];

                for (int i = 0; i < inValidChoices.Length; i++)
                {
                    inValidChoices[i] = new List<int>();
                }
            }
        }

        private void CreateGridRows()
        {
            foreach (Transform child in _transform)
            {
                Destroy(child.gameObject);
            }
            _transform.DetachChildren();

            float rowPositionY = (grid.RowCount & 1) != 0 ? _transform.position.y + grid.RowCount / 2 * slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleY
                : _transform.position.y + (grid.RowCount - 1) / 2 * slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleY + 0.5f * slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleY;

            int[] spriteArray = GenerateSpriteIndexArray();

            for (int i = 0; i < grid.RowCount; i++)
            {
                CreateGridRow(rowPositionY, i * grid.ColumnCount, spriteArray);

                rowPositionY -= slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleY;
            }
        }

        private void CreateGridRow(float yPos, int currentIndex, int[] spriteArray)
        {
            Vector3 startPosition = (grid.ColumnCount & 1) != 0 ? _transform.position
                : _transform.position - Vector3.right * slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleX / 2f;

            startPosition.y = yPos;

            Transform slotInstance = Instantiate(slotPrefab, _transform);

            slotInstance.position = startPosition;

            SpriteRenderer slotSpriteRenderer = slotInstance.GetComponentInChildren<SpriteRenderer>();
            slotSpriteRenderer.sprite =
                slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas[spriteArray[currentIndex]].SlotSprite;

            if (slotBundleData[randomizer.RandomSlotBundleIndex].RotateWidthHeight && 
                slotSpriteRenderer.sprite.rect.width > slotSpriteRenderer.sprite.rect.height)
                slotInstance.Rotate(-90f * Vector3.forward);

            if (randomizer.RandomIndex == _transform.childCount - 1)
                textSetter.SetText(slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas[spriteArray[currentIndex]].SlotName);

            InstantiateSideSlots(startPosition, currentIndex, spriteArray);
        }

        private void InstantiateSideSlots(Vector3 startPosition, int currentIndex, int[] spriteArray)
        {
            int sideToAdd = 1;

            for (int i = 0; i < grid.ColumnCount - 1; i++)
            {
                Transform slotInstance = Instantiate(slotPrefab, _transform);

                slotInstance.position = startPosition + sideToAdd * ((1 + i / 2) * slotBundleData[randomizer.RandomSlotBundleIndex].SlotScaleX * Vector3.right);

                SpriteRenderer slotSpriteRenderer = slotInstance.GetComponentInChildren<SpriteRenderer>();
                slotSpriteRenderer.sprite =
                    slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas[spriteArray[currentIndex + i + 1]].SlotSprite;

                if (slotBundleData[randomizer.RandomSlotBundleIndex].RotateWidthHeight && 
                    slotSpriteRenderer.sprite.rect.width > slotSpriteRenderer.sprite.rect.height)
                    slotInstance.Rotate(-90f * Vector3.forward);

                if (randomizer.RandomIndex == _transform.childCount - 1)
                    textSetter.SetText(slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas[spriteArray[currentIndex + i + 1]].SlotName);

                sideToAdd *= -1;
            }
        }

        private int[] GenerateSpriteIndexArray()
        {
            int[] spriteArray = new int[grid.RowCount * grid.ColumnCount];
            int[] availableArray = new int[slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas.Length];

            for (int i = 0; i < slotBundleData[randomizer.RandomSlotBundleIndex].SlotDatas.Length; i++)
            {
                availableArray[i] = i;
            }

            for (int arrayIndex = 0, availableArrayIndex = availableArray.Length; arrayIndex < spriteArray.Length; arrayIndex++)
            {
                int tempIndex = Random.Range(0, availableArrayIndex);
                --availableArrayIndex;

                int tempValue = availableArray[availableArrayIndex];
                availableArray[availableArrayIndex] = availableArray[tempIndex];
                availableArray[tempIndex] = tempValue;

                spriteArray[arrayIndex] = availableArray[availableArrayIndex];
            }

            if (inValidChoices[randomizer.RandomSlotBundleIndex].Exists(value => value == spriteArray[randomizer.RandomIndex]))
            {
                for (int i = 0; i < availableArray.Length; i++)
                {
                    if (!inValidChoices[randomizer.RandomSlotBundleIndex].Exists(value => value == availableArray[i]))
                    {
                        int arrayIndex = Array.FindIndex(spriteArray, index => index == availableArray[i]);

                        if (arrayIndex != -1)
                        {
                            int tempValue = spriteArray[randomizer.RandomIndex];
                            spriteArray[randomizer.RandomIndex] = spriteArray[arrayIndex];
                            spriteArray[arrayIndex] = tempValue;
                        }
                        else
                        {
                            spriteArray[randomizer.RandomIndex] = availableArray[i];
                        }

                        inValidChoices[randomizer.RandomSlotBundleIndex].Add(spriteArray[randomizer.RandomIndex]);

                        return spriteArray;
                    }
                }

                GenerateInValidChoices();
            }

            inValidChoices[randomizer.RandomSlotBundleIndex].Add(spriteArray[randomizer.RandomIndex]);

            return spriteArray;
        }
    }
}
