# JMeter Performance Testing Guide

## Test Configuration

### Scenarios:
1. **VnExpress Homepage - 500 Users**
   - Ramp-up time: 60 seconds
   - Duration: 300 seconds (5 minutes)
   - Requests per user: 2 (Homepage + Search)

2. **Zing News Homepage - 500 Users**
   - Ramp-up time: 60 seconds
   - Duration: 300 seconds (5 minutes)
   - Requests per user: 1 (Homepage)

### Load Profile:
- **Light Load**: 100 users
- **Medium Load**: 500 users
- **Heavy Load**: 1000 users

### Performance Metrics to Monitor:
1. **Response Time**: Average, Min, Max, 95th percentile
2. **Throughput**: Requests per second
3. **Error Rate**: Failed requests percentage
4. **CPU & Memory**: Server resource usage

### Acceptance Criteria:
- Average Response Time < 2 seconds
- 95th percentile Response Time < 5 seconds
- Error Rate < 1%
- Throughput > 50 requests/second

## Running the Test:

```bash
# GUI Mode (for creating/editing test plan):
jmeter -t NewsWebsite_PerformanceTest.jmx

# Command Line Mode (for production testing):
jmeter -n -t NewsWebsite_PerformanceTest.jmx -l results.jtl -j jmeter.log -e -o results_html

# With custom properties:
jmeter -n -t NewsWebsite_PerformanceTest.jmx -l results.jtl -Jjmeter.save.saveall=true
```

## Interpreting Results:

1. **Summary Report**:
   - Label: Request name
   - Samples: Number of requests
   - Average: Mean response time
   - Min: Minimum response time
   - Max: Maximum response time
   - Std. Dev.: Standard deviation
   - Error Rate: Percentage of failed requests
   - Throughput: Requests per second
   - Received KB/sec: Data received per second
   - Sent KB/sec: Data sent per second

2. **Graph Results**:
   - Response Time Graph: Shows performance over time
   - Throughput Graph: Shows load over time

## Troubleshooting:

1. **High Error Rate**:
   - Check if website is accessible
   - Verify network connectivity
   - Check website server logs

2. **Slow Response Time**:
   - May indicate server bottleneck
   - Check server resources (CPU, Memory, Disk I/O)
   - Consider load balancing

3. **Connection Timeouts**:
   - Increase connection timeout
   - Check if website can handle concurrent connections
   - Verify firewall rules

## Next Steps:

1. Run tests with different load profiles
2. Analyze results and identify bottlenecks
3. Tune server configurations if needed
4. Run stress tests to find breaking point
5. Document findings and recommendations
