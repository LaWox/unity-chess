using UnityEngine;

namespace PlayerPieces
{
    public class PieceGenerator : MonoBehaviour
    {
        public GameObject pawnPrefab;
        public GameObject kingPrefab;
        public GameObject queenPrefab;
        public GameObject rookPrefab;
        public GameObject bishopPrefab;
        public GameObject knightPrefab;

        public Material materialWhite;
        public Material materialBlack;
        private GridHandler _gridHandler;
        private PointerHandler _pointerHandler;

        private void Start()
        {
            _pointerHandler = FindFirstObjectByType<PointerHandler>();
            _gridHandler = FindFirstObjectByType<GridHandler>();

            for (var i = 0; i < 8; i++)
            {
                var pawnPieceWhite = Instantiate(pawnPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                    .GetComponent<Movable>();
                var pawnPieceBlack = Instantiate(pawnPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                    .GetComponent<Movable>();
                Movable otherPieceWhite = null;
                Movable otherPieceBlack = null;

                switch (i)
                {
                    case 0 or 7:
                        otherPieceWhite = Instantiate(rookPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        otherPieceBlack = Instantiate(rookPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        break;
                    case 1 or 6:
                        otherPieceWhite = Instantiate(knightPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        otherPieceBlack = Instantiate(knightPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        break;
                    case 2 or 5:
                        otherPieceWhite = Instantiate(bishopPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();

                        otherPieceBlack = Instantiate(bishopPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        break;
                    case 3:
                        otherPieceWhite = Instantiate(queenPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        otherPieceBlack = Instantiate(queenPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        break;
                    case 4:
                        otherPieceWhite = Instantiate(kingPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        otherPieceBlack = Instantiate(kingPrefab, new Vector3(i, 0, 0), Quaternion.identity)
                            .GetComponent<Movable>();
                        break;
                }

                pawnPieceWhite.pointerHandler = _pointerHandler;
                pawnPieceWhite.gridHandler = _gridHandler;
                pawnPieceWhite.startPos = new Vector2Int(i, 1);
                pawnPieceWhite.GetComponent<Renderer>().material = materialWhite;

                pawnPieceBlack.pointerHandler = _pointerHandler;
                pawnPieceBlack.gridHandler = _gridHandler;
                pawnPieceBlack.startPos = new Vector2Int(i, 6);
                pawnPieceBlack.GetComponent<Renderer>().material = materialBlack;

                if (!otherPieceWhite || !otherPieceBlack) continue;

                otherPieceWhite.pointerHandler = _pointerHandler;
                otherPieceWhite.gridHandler = _gridHandler;
                otherPieceWhite.startPos = new Vector2Int(i, 0);
                otherPieceWhite.GetComponent<Renderer>().material = materialWhite;

                otherPieceBlack.pointerHandler = _pointerHandler;
                otherPieceBlack.gridHandler = _gridHandler;
                otherPieceBlack.startPos = new Vector2Int(i, 7);
                otherPieceBlack.GetComponent<Renderer>().material = materialBlack;
            }
        }
    }
}