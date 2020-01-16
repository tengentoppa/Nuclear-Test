namespace RobotSettingTest.DTO
{
    public class PathTreeNode
    {
        public string Text { get; private set; }
        public string Path { get; private set; }

        public PathTreeNode(string text, string path)
        {
            Text = text;
            Path = path;
        }
    }

    public class NumberTreeNode
    {
        public string Text { get; set; }
        public int Num { get; set; }

        public NumberTreeNode(string text, int num)
        {
            Text = text;
            Num = num;
        }
    }
}
