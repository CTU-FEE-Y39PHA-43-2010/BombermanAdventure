using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BombermanAdventure.Cameras
{
    public class Camera
    {
        /// <summary>
        /// vycet pohledu
        /// </summary>
        public enum Position { FRONT=1, LEFT, BACK, RIGHT };

        /// <summary>
        /// inicializacni vektor pro kamery
        /// slouzi k hromadnemu nastaveni pohledu
        /// </summary>
        private Vector3 initializePositionVector = new Vector3(275, 400, 0);

        /// <summary>
        /// vektor pro predni pohled
        /// </summary>
        private Vector3 cameraPositionFront;
        
        /// <summary>
        /// vektor pro levy pohled
        /// </summary>
        private Vector3 cameraPositionLeft;

        /// <summary>
        /// vektor pro zadni pohled
        /// </summary>
        private Vector3 cameraPositionBack;
        
        /// <summary>
        /// vektor pro pravy pohled
        /// </summary>
        private Vector3 cameraPositionRight;

        /// <summary>
        /// aktualni pozice kamery
        /// </summary>
        Position _position;

        public Position position 
        {
            get { return _position; }
        }

        /// <summary>
        /// promena pro urceni jednoho stisku klavesy 
        /// </summary>
        KeyboardState oldState;

        /// <summary>
        /// vektor, ktery urcuje kam kamera smeruje
        /// </summary>
        Vector3 cameraTar;

        /// <summary>
        /// rozdil vektoru smeru kamery
        /// </summary>
        Vector3 deltaCameraTar;

        /// <summary>
        /// projekcni matice
        /// </summary>
        public Matrix projectionMatrix;

        /// <summary>
        /// pohledova matice
        /// </summary>
        public Matrix viewMatrix = Matrix.Identity;
        
        /// <summary>
        /// 
        /// </summary>
        float aspectRatio;

        Vector3 cameraPosition;
        Vector3 cameraOldPostion;

        public Vector3 CameraPosition 
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        public Camera(float aspectRatio) 
        {
            this.Initialize(aspectRatio);
        }

        private void Initialize(float aspectRatio) 
        {
            this.aspectRatio = aspectRatio;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), this.aspectRatio, 1f, 1000f);

            //pocatecni pozice kamery je zepredu
            _position = Position.FRONT;
            deltaCameraTar = new Vector3();
            //cameraPosition = new Vector3(150, 100, 350);

            cameraPositionFront = new Vector3(initializePositionVector.X, initializePositionVector.Y, initializePositionVector.Z);
            cameraPositionLeft = new Vector3(initializePositionVector.Z, initializePositionVector.Y, -initializePositionVector.X);
            cameraPositionBack = new Vector3(-initializePositionVector.X, initializePositionVector.Y, initializePositionVector.Z);
            cameraPositionRight = new Vector3(initializePositionVector.Z, initializePositionVector.Y, initializePositionVector.X);
        }

        public void Update() 
        {
            this.SetCameraPosition();   
            cameraTar = new Vector3(cameraPosition.X + deltaCameraTar.X, cameraPosition.Y + deltaCameraTar.Y, cameraPosition.Z + deltaCameraTar.Z);
            viewMatrix = Matrix.CreateLookAt(new Vector3(cameraPosition.X, cameraPosition.Y, cameraPosition.Z), cameraTar, Vector3.Up);

            HandleKeyboardInput();
            //HandleMouseInput();
        }

        private void SetCameraPosition() 
        {
            switch (_position) 
            {
                case Position.FRONT:
                    cameraPosition = cameraPositionFront;
                    deltaCameraTar.X = -3.0f;
                    deltaCameraTar.Y = -5.0f;
                    deltaCameraTar.Z = 0f;
                    break;
                case Position.LEFT:
                    cameraPosition = cameraPositionLeft;
                    deltaCameraTar.X = 0f;
                    deltaCameraTar.Y = -5.0f;
                    deltaCameraTar.Z = 3.0f;
                    break;
                case Position.BACK:
                    cameraPosition = cameraPositionBack;
                    deltaCameraTar.X = 3.0f;
                    deltaCameraTar.Y = -5.0f;
                    deltaCameraTar.Z = 0.0f;
                    break;
                case Position.RIGHT:
                    cameraPosition = cameraPositionRight;
                    deltaCameraTar.X = 0f;
                    deltaCameraTar.Y = -5.0f;
                    deltaCameraTar.Z = -3.0f;
                    break;
            }
        } 

        private void Left() 
        {
            switch (_position)
            {
                case Position.FRONT:
                    _position = Position.LEFT;
                    System.Console.Beep();
                    System.Console.Beep();
                    break;
                case Position.LEFT:
                    _position = Position.BACK;
                    System.Console.Beep();
                    System.Console.Beep();
                    System.Console.Beep();
                    break;
                case Position.BACK:
                    _position = Position.RIGHT;
                    System.Console.Beep();
                    System.Console.Beep();
                    System.Console.Beep();
                    System.Console.Beep();
                    break;
                case Position.RIGHT:
                    _position = Position.FRONT;
                    System.Console.Beep();
                    break;
            }
        }

        private void Right() 
        {
            switch (_position)
            {
                case Position.FRONT:
                    _position = Position.RIGHT;
                    break;
                case Position.RIGHT:
                    _position = Position.BACK;
                    break;
                case Position.BACK:
                    _position = Position.LEFT;
                    break;
                case Position.LEFT:
                    _position = Position.FRONT;
                    break;
            }
        }

        private void Top() 
        {
            cameraOldPostion = cameraPosition;

        }


        private void HandleKeyboardInput() 
        {
            KeyboardState ks = Keyboard.GetState();


            if (ks.IsKeyDown(Keys.N))
            {
                if (!oldState.IsKeyDown(Keys.N))
                {
                    this.Left();
                }
            }

            if (ks.IsKeyDown(Keys.M))
            {
                if (!oldState.IsKeyDown(Keys.M))
                {
                    this.Right();
                }
            }

            oldState = ks;
            
        }

        
        /*private void HandleMouseInput()
        {
            float partX = 20;
            float partY = 20;
            MouseState ms = Mouse.GetState();

            // Scroll-Down | Zoom Out
            if (ms.ScrollWheelValue < lastMouseScrollValue)
            {
                cameraPosition.Y += scrollSpeed;
                //horni hranice
                if (CameraPosition.Y > 150) cameraPosition.Y = 150;
            }

            // Scroll-Up | Zoom In
            if (ms.ScrollWheelValue > lastMouseScrollValue)
            {
                cameraPosition.Y -= scrollSpeed;
                //spodni hranice
                if (CameraPosition.Y < 40) cameraPosition.Y = 40;
            }

            //update last position
            lastMouseScrollValue = ms.ScrollWheelValue;

            //right
            if (ms.X > (GameClass.ScreenWidth - partX))
            {
                cameraPosition.X += cameraSpeed;
            }

            //left
            if (ms.X < partX)
            {
                cameraPosition.X -= cameraSpeed;
            }

            //up
            if (ms.Y > (GameClass.ScreenHeight - partY))
            {
                cameraPosition.Z += cameraSpeed;
            }

            //down
            if (ms.Y < partY)
            {
                cameraPosition.Z -= cameraSpeed;
            }
        }*/
    }
}
