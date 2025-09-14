using awing_test.Server.Models.DTOs;

namespace awing_test.Server.Handles
{
    public class HandleCalculate
    {
        public HandleCalculate() { }

        /// <summary>
        /// Thực hiện tính toán cho value bé nhất
        /// </summary>
        /// <param name="calculateRequest"></param>
        /// <returns></returns>
        public double DoCalculate(CalculateRequestDTO calculateRequest)
        {
            int n = calculateRequest.N;
            int m = calculateRequest.M;
            int p = calculateRequest.P;
            int[][] a = calculateRequest.Matrix;

            // Danh sách vị trí cho từng loại rương (1..p)
            var pos = new List<(int x, int y)>[p + 1];
            for (int i = 0; i <= p; i++)
                pos[i] = new List<(int, int)>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int chest = a[i][j];
                    pos[chest].Add((i, j));
                }
            }

            // DP lưu chi phí nhỏ nhất đến từng rương số k
            var dp = new List<double>[p + 1];
            for (int i = 0; i <= p; i++)
                dp[i] = new List<double>();

            // Khởi tạo dp[1] (bắt đầu từ (0,0) vì (1,1) trong đề = index (0,0))
            foreach (var (x, y) in pos[1])
            {
                double dist = Math.Sqrt(x * x + y * y);
                dp[1].Add(dist);
            }

            // Tính dần từ rương 2 đến p
            for (int k = 2; k <= p; k++)
            {
                dp[k] = Enumerable.Repeat(double.MaxValue, pos[k].Count).ToList();

                for (int i = 0; i < pos[k].Count; i++)
                {
                    var (x2, y2) = pos[k][i];

                    for (int j = 0; j < pos[k - 1].Count; j++)
                    {
                        var (x1, y1) = pos[k - 1][j];
                        double dist = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
                        double cand = dp[k - 1][j] + dist;

                        if (cand < dp[k][i])
                            dp[k][i] = cand;
                    }
                }
            }

            return dp[p].Min();
        }
    }
}