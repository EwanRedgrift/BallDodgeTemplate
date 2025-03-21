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
    public int width = 20;
    public int height = 20;
    public int speed = 6;

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

        if (direction == "right" && x < GameScreen.screenWidth - width)
        {
            x += speed;
        }
        else if (direction == "left" && x > 0)
        {
            x -= speed;
        }
        else if (direction == "up" && y > 0)
        {
            y -= speed;
        }
        else if (direction == "down" && y < GameScreen.screenHeight - height)
        {
            y += speed;
        }

        // Move body segments to follow the player, spaced out
        if (bodyParts.Count > 0)
        {
            for (int i = bodyParts.Count - 1; i > 0; i--)
            {
                bodyParts[i].x = bodyParts[i - 1].x;
                bodyParts[i].y = bodyParts[i - 1].y;
            }
            // Move the first body segment to where the player was
            bodyParts[0].x = prevX;
            bodyParts[0].y = prevY;
        }

        // Update the heading
        heading = direction;
    }

    public bool Collision(Ball b)
    {
        // Check collision between player (head) and ball
        Rectangle heroRec = new Rectangle(x, y, width, height);
        Rectangle chaseRec = new Rectangle(b.row, b.column, b.size, b.size);

        if (heroRec.IntersectsWith(chaseRec))
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
        // Spacing factor between body parts
        int spacing = 25;

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

}
