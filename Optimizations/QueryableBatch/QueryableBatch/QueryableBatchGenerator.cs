namespace QueryableBatch;

public class QueryableBatchGenerator
{
    public static List<IQueryable<T>> Generate<T>(IQueryable<T> query, int batchSize)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));
        if (batchSize <= 0) throw new ArgumentException("Batch size must be greater than 0.", nameof(batchSize));

        List<IQueryable<T>> batches = new List<IQueryable<T>>();

        for (var i = 0; i < query.Count(); i += batchSize)
        {
            batches.Add(query.Skip(i).Take(batchSize));
        }

        return batches;
    }
}