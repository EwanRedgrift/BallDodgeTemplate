using BallDodgeTemplate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Player
{
    public int x, y;
    public int width = 100;
    public int height = 100;
    public int speed = 10;

    int spacing = 100;

    public string heading = "up";

    public List<Body> bodyParts = new List<Body>(); // List to store body segments

    public Player()
    {
        x = GameScreen.screenWidth / 2 - width / 2;
        y = GameScreen.screenHeight / 2 - height / 2;
    }

    public void ChangeDirection(string direction)
    {
        heading = direction;
    }

    public void Move(string direction)
    {
        int prevX = x;
        int prevY = y;

        // Move the player (head) based on direction
        switch (direction)
        {
            case "right":
                if (x < GameScreen.screenWidth - width) x += 100; // Move by 50 pixels, slower speed
                break;
            case "left":
                if (x > 0) x -= 25; // Move by 50 pixels, slower speed
                break;
            case "up":
                if (y > 0) y -= 25; // Move by 50 pixels, slower speed
                break;
            case "down":
                if (y < GameScreen.screenHeight - height) y += 100; // Move by 100 pixels, slower speed
                break;
        }

        // Ensure the player is aligned to the grid (multiples of 100)
        x = (x / 100) * 100; // Round to nearest grid multiple (100 pixels step size)
        y = (y / 100) * 100; // Round to nearest grid multiple (100 pixels step size)

        // Move body parts (follow the player in reverse order)
        if (bodyParts.Count > 0)
        {
            for (int i = bodyParts.Count - 1; i > 0; i--)
            {
                // Move each body part to the position of the one in front of it
                bodyParts[i].x = bodyParts[i - 1].x;
                bodyParts[i].y = bodyParts[i - 1].y;
            }

            // The first body part should take the previous head position, but spaced out
            bodyParts[0].x = prevX;
            bodyParts[0].y = prevY;
        }
    }



    public bool Collision(Ball b)
    {
        // Check collision between player (head) and ball
        Rectangle heroRec = new Rectangle(x, y, width, height);
        Rectangle foodRec = new Rectangle(b.row, b.column, b.size, b.size);

        if (heroRec.IntersectsWith(foodRec))
        {
            // Player eats the ball, increase score and grow (add a body segment)
            GameScreen.points++;

            // Add a new body segment with spacing
            AddBodySegment();

            return true;
        }

        return false;
    }


    private void AddBodySegment()
    {

        // Create new body segment and position it at the current head, with spacing
        int newX;
        int newY;

        if (bodyParts.Count > 0)
        {
            // Follow last body part if it exists
            newX = bodyParts[bodyParts.Count - 1].x;
            newY = bodyParts[bodyParts.Count - 1].y;
        }
        else
        {
            // If no body parts, use the player's position
            newX = x;
            newY = y;
        }

        // Adjust position based on current direction
        switch (heading)
        {
            case "up":
                newY += spacing;
                break;
            case "down":
                newY -= spacing;
                break;
            case "left":
                newX += spacing;
                break;
            case "right":
                newX -= spacing;
                break;
        }

        // Add new body segment at the calculated position
        Body newBodySegment = new Body(newX, newY);
        bodyParts.Add(newBodySegment);
    }

    public bool CheckBodyCollision()
    {
        // Check if the player collides with any of its body parts (excluding the head)
        for (int i = 1; i < bodyParts.Count; i++) // Start from 1 to skip the head (index 0)
        {
            Rectangle headRect = new Rectangle(x, y, width, height);
            Rectangle bodyRect = new Rectangle(bodyParts[i].x, bodyParts[i].y, bodyParts[i].width, bodyParts[i].height);

            if (headRect.IntersectsWith(bodyRect)) // Check if the head intersects with the body part
            {
                GameScreen.points += 1000; // Add points when the player collides with itself
                return true;
            }
        }
        return false;
    }
}