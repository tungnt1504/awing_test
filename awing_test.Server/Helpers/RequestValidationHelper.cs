using awing_test.Server.Models.DTOs;

public class RequestValidationHelper
{
    /// <summary>
    /// Validate đầu vào của API
    /// </summary>
    /// <param name="calculateRequest"></param>
    /// <returns></returns>
    public (bool IsError, string Message) CalculateRequestValidate(CalculateRequestDTO calculateRequest)
    {
        bool isError = false;
        string message = string.Empty;

        if (calculateRequest != null)
        {
            int mInt;
            int nInt;
            int pInt;

            // check for m
            // check if m is null or empty
            if (String.IsNullOrEmpty(calculateRequest.M.ToString()))
            {
                message += "M cannot be null or empty.";
                isError = true;
            }
            else
            {
                // validate if m is an integer
                if (!Int32.TryParse(calculateRequest.M.ToString(), out mInt))
                {
                    message += "M must be an integer.";
                    isError = true;
                }
            }

            // check for n
            // check if n is null or empty
            if (String.IsNullOrEmpty(calculateRequest.N.ToString()))
            {
                message += "N cannot be null or empty.";
                isError = true;
            }
            else
            {
                // validate if n is an integer
                if (!Int32.TryParse(calculateRequest.N.ToString(), out mInt))
                {
                    message += "N must be an integer.";
                    isError = true;
                }
            }

            // check for p
            // check if p is null or empty
            if (String.IsNullOrEmpty(calculateRequest.P.ToString()))
            {
                message += "P cannot be null or empty.";
                isError = true;
            }
            else
            {
                // validate if p is an integer
                if (!Int32.TryParse(calculateRequest.P.ToString(), out mInt))
                {
                    message += "P must be an integer.";
                    isError = true;
                }
            }

            // check for matrix
            if (calculateRequest.Matrix == null || calculateRequest.Matrix.Length != calculateRequest.N)
            {
                message += $"Matrix must have exactly {calculateRequest.N} rows.";
                isError = true;
            }
            else
            {
                for (int i = 0; i < calculateRequest.N; i++)
                {
                    if (calculateRequest.Matrix[i] == null || calculateRequest.Matrix[i].Length != calculateRequest.M)
                    {
                        message += $"Row {i + 1} must have exactly {calculateRequest.M} columns.";
                        isError = true;
                    }

                    for (int j = 0; j < calculateRequest.Matrix[i].Length; j++)
                    {
                        if (calculateRequest.Matrix[i][j] < 1 || calculateRequest.Matrix[i][j] > calculateRequest.P)
                        {
                            message += $"Matrix[{i},{j}] must be between 1 and {calculateRequest.P}.";
                            isError = true;
                        }
                    }
                }
            }
        }


        return (isError, message);
    }
}