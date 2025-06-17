using GameHandler;
using Grid;
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
        private BoardHandler _boardHandler;
        private GridHandler _gridHandler;
        private PointerHandler _pointerHandler;

        private void Start()
        {
            _boardHandler = FindFirstObjectByType<BoardHandler>();
            _pointerHandler = FindFirstObjectByType<PointerHandler>();
            _gridHandler = FindFirstObjectByType<GridHandler>();

            for (var i = 0; i < 8; i++)
            {
                var pawnPieceWhite = Instantiate(pawnPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                var pawnMovableComponentWhite = pawnPieceWhite.GetComponent<Movable>();
                var pawnPieceComponentWhite = pawnPieceWhite.GetComponent<PlayerPiece>();

                var pawnPieceBlack = Instantiate(pawnPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                var pawnMovableComponentBlack = pawnPieceBlack.GetComponent<Movable>();
                var pawnPieceComponentBlack = pawnPieceBlack.GetComponent<PlayerPiece>();

                GameObject otherPieceWhite = null;
                GameObject otherPieceBlack = null;

                switch (i)
                {
                    case 0 or 7:
                        otherPieceWhite = Instantiate(rookPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        otherPieceBlack = Instantiate(rookPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        break;
                    case 1 or 6:
                        otherPieceWhite = Instantiate(knightPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        otherPieceBlack = Instantiate(knightPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        break;
                    case 2 or 5:
                        otherPieceWhite = Instantiate(bishopPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        otherPieceBlack = Instantiate(bishopPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        break;
                    case 3:
                        otherPieceWhite = Instantiate(queenPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        otherPieceBlack = Instantiate(queenPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        break;
                    case 4:
                        otherPieceWhite = Instantiate(kingPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        otherPieceBlack = Instantiate(kingPrefab, new Vector3(i, 0, 0), Quaternion.identity);
                        break;
                }

                // white
                pawnPieceWhite.transform.position = _gridHandler.GetWorldPositionFromCellIndex(new Vector2Int(i, 1));
                ;
                pawnPieceWhite.GetComponent<Renderer>().material = materialWhite;
                pawnPieceComponentWhite.Initialize(true, new Vector2Int(i, 1));
                _boardHandler.SetCellState(new Vector2Int(i, 1), pawnPieceComponentWhite);

                pawnMovableComponentWhite.pointerHandler = _pointerHandler;
                pawnMovableComponentWhite.gridHandler = _gridHandler;

                // black
                pawnPieceBlack.transform.position = _gridHandler.GetWorldPositionFromCellIndex(new Vector2Int(i, 6));
                ;
                pawnPieceBlack.GetComponent<Renderer>().material = materialBlack;
                pawnPieceComponentBlack.Initialize(false, new Vector2Int(i, 6));
                _boardHandler.SetCellState(new Vector2Int(i, 6), pawnPieceComponentBlack);

                pawnMovableComponentBlack.pointerHandler = _pointerHandler;
                pawnMovableComponentBlack.gridHandler = _gridHandler;

                // other pieces
                if (!otherPieceWhite || !otherPieceBlack) continue;

                // white
                var pieceComponentWhite = otherPieceWhite.GetComponent<PlayerPiece>();
                pieceComponentWhite.Initialize(true, new Vector2Int(i, 0));
                _boardHandler.SetCellState(new Vector2Int(i, 0), pieceComponentWhite);


                otherPieceWhite.transform.position = _gridHandler.GetWorldPositionFromCellIndex(new Vector2Int(i, 0));
                otherPieceWhite.GetComponent<Renderer>().material = materialWhite;

                var otherMovableWhite = otherPieceWhite.GetComponent<Movable>();
                otherMovableWhite.pointerHandler = _pointerHandler;
                otherMovableWhite.gridHandler = _gridHandler;

                // black
                var pieceComponentBlack = otherPieceBlack.GetComponent<PlayerPiece>();
                pieceComponentBlack.Initialize(false, new Vector2Int(i, 7));
                _boardHandler.SetCellState(new Vector2Int(i, 7), pieceComponentBlack);

                var otherMovableBlack = otherPieceBlack.GetComponent<Movable>();
                otherPieceBlack.GetComponent<Renderer>().material = materialBlack;

                otherPieceBlack.transform.position = _gridHandler.GetWorldPositionFromCellIndex(new Vector2Int(i, 7));
                otherMovableBlack.pointerHandler = _pointerHandler;
                otherMovableBlack.gridHandler = _gridHandler;
            }
        }
    }
}